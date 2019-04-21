using CrcAspNetCore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CrcAspNetCore.Api.DbContexts
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}
