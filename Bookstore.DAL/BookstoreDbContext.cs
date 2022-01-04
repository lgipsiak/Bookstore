using Bookstore.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DAL
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
