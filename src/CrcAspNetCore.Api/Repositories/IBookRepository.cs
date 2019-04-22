using System.Collections.Generic;
using System.Threading.Tasks;
using CrcAspNetCore.Api.Models;

namespace CrcAspNetCore.Api.Repositories
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<Book> GetByIsbnAsync(string isbn);
        Task<IEnumerable<Book>> SearchByPhraseAsync(string phrase);
        Task AddAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();
    }
}
