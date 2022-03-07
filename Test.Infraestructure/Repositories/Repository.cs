using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.Infraestructure.Database;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Specifications;

namespace Test.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected StoreContext _storeContext;
        public Repository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task InsertAsync(T entity)
        {
            await _storeContext.Set<T>().AddAsync(entity);
            await _storeContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, ISpecification<T> specification) {
            var item = await ApplySpecification(specification).FirstOrDefaultAsync();
            if (item != null)
            {
                _storeContext.Entry(item).CurrentValues.SetValues(entity);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(ISpecification<T> specification)
        {
            var item = await ApplySpecification(specification).FirstOrDefaultAsync();
            if (item != null)
            {
                _storeContext.Set<T>().Remove(item);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
        {
            return specification != null ? await ApplySpecification(specification).ToListAsync() : await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return Evaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);
        }
    }
}