using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrcAspNetCore.Api.DbContexts;
using CrcAspNetCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CrcAspNetCore.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }
        public async Task<Book> GetByIdAsync(int id)
            => await _bookContext.Books.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Book> GetByIsbnAsync(string isbn)
            => await _bookContext.Books.FirstOrDefaultAsync(x => x.Isbn == isbn);

        public async Task<IEnumerable<Book>> SearchByPhraseAsync(string phrase)
            => await _bookContext.Books.Where(x => x.Title.Contains(phrase)
                                                   || x.Author.Contains(phrase)).Take(15).ToListAsync();

        public async Task AddAsync(Book book)
        {
            await _bookContext.Books.AddAsync(book);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            _bookContext.Books.Remove(book);
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return await _bookContext.SaveChangesAsync() > 0;
        }
    }
}
