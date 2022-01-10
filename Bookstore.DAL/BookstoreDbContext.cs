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
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();

            modelBuilder.Entity<Author>()
            .Property(a => a.FirstName)
            .IsRequired();

            modelBuilder.Entity<Author>()
            .Property(a => a.LastName)
            .IsRequired();
        }
    }
}
