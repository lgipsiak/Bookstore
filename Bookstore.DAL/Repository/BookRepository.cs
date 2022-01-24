using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.DAL.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public BookRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Book book)
        {
            await _dbContext.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books.Include(x => x.Book_Author)
                                         .ThenInclude(x => x.Author)
                                         .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
