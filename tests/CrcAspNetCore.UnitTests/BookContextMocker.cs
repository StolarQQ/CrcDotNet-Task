using CrcAspNetCore.Api.DbContexts;
using CrcAspNetCore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrcAspNetCore.UnitTests
{
    public static class BookContextMocker
    {
        public static IBookRepository GetInMemoryBookRepository(string dbName)
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var bookContext = new BookContext(options);
            bookContext.FillDatabase();
            
            return new BookRepository(bookContext);
        }
    }
}
