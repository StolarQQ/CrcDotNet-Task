using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Book>> GetAllAsync()
            => await _bookContext.Books.ToListAsync();

        public async Task AddAsync(Book book)
        {
            await _bookContext.Books.AddAsync(book);
            await _bookContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Book book)
        {
            var OldBook = await GetByIdAsync(id);
           
            OldBook.Title = book.Title;
            OldBook.Author = book.Author;
            OldBook.Description = book.Description;
            OldBook.Isbn = book.Isbn;
            OldBook.PublishedAt = book.PublishedAt;

            await _bookContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            _bookContext.Books.Remove(book);
            await _bookContext.SaveChangesAsync();
        }
    }
}
