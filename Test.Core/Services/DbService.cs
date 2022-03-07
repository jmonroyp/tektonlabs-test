using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Test.Infraestructure.Entities;
using Test.Infraestructure.Repositories;
using Test.Infraestructure.Specifications;

namespace Test.Core.Services
{
    public abstract class DbService<Dto, Entity> : IDbService<Dto, Entity> where Entity : BaseEntity
    {
        private readonly IRepository<Entity> _repo;
        private readonly IMapper _mapper;

        public DbService(IRepository<Entity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<Dto>> GetAllAsync()
        {
            return _mapper
            .Map<List<Entity>, List<Dto>>(await _repo.GetAllAsync(null));
        }

        public async Task<Dto> GetByIdAsync(ISpecification<Entity> spec)
        {
            return _mapper.Map<Entity, Dto>(await _repo.GetAsync(spec));;
        }

        public async Task InsertAsync(Dto dto)
        {
            await _repo.InsertAsync(_mapper.Map<Dto, Entity>(dto));
        }

        public async Task UpdateAsync(Dto dto, ISpecification<Entity> spec)
        {
            await _repo
            .UpdateAsync(_mapper.Map<Dto, Entity>(dto), spec);
        }

        public async Task RemoveByIdAsync(ISpecification<Entity> spec)
        {
            await _repo.RemoveAsync(spec);
        }
    }
}