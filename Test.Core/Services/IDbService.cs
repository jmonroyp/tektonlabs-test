using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Core.Services
{
    public interface IDbService<T>
    {
        Task InsertAsync(T dto);
        Task UpdateAsync(T dto);
        Task RemoveByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id, string storeApi);      
    }
}