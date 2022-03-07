using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Infraestructure.Specifications;

namespace Test.Core.Services
{
    public interface IDbService<Dto, Entity>
    {
        Task InsertAsync(Dto dto);
        Task UpdateAsync(Dto dto, ISpecification<Entity> spec);
        Task RemoveByIdAsync(ISpecification<Entity> spec);
        Task<List<Dto>> GetAllAsync();
        Task<Dto> GetByIdAsync(ISpecification<Entity> spec);      
    }
}