using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Infraestructure.Specifications;

namespace Test.Core.Services
{
    public interface IProxyService<Dto, ExternalDto>
    {
        Task<List<Dto>> GetAllAsync(string path);
        Task<Dto> GetByIdAsync(string path);   
    }
}