using System.Collections.Generic;
using System.Threading.Tasks;
using CrcAspNetCore.Api.Models;

namespace CrcAspNetCore.Api.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task AddAsync(Book book);
        Task UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);
    }
}
