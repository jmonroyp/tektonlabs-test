using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Specifications;

namespace Test.Infraestructure.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity, ISpecification<T> specification);
        Task RemoveAsync(ISpecification<T> specification);
        Task<List<T>> GetAllAsync(ISpecification<T> specification);
        Task<T> GetAsync(ISpecification<T> specification);

    }
}