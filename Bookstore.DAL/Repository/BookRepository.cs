using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return await _dbContext.Books.Include(x => x.Book_Tag)
                                         .ThenInclude(x => x.Tag)
                                         .Include(x => x.Book_Author.OrderBy(x => x.Author.LastName))
                                         .ThenInclude(x => x.Author)
                                         .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync(BookQuery query)
        {
            return await _dbContext.Books.Include(x => x.Book_Tag)
                                         .ThenInclude(x => x.Tag)
                                         .Include(x => x.Book_Author.OrderBy(x => x.Author.LastName))
                                         .ThenInclude(x => x.Author)
                                         .Where(x => query.SearchPhrase == null
                                                      || (x.Title.ToLower().Contains(query.SearchPhrase.ToLower()))
                                                      || (x.Book_Tag.Any(x => x.Tag.Message.ToLower().Contains(query.SearchPhrase.ToLower()))
                                                      || (x.Book_Author.Any(x => x.Author.FirstName.ToLower().Contains(query.SearchPhrase.ToLower()))
                                                      || (x.Book_Author.Any(x => x.Author.LastName.ToLower().Contains(query.SearchPhrase.ToLower()))))))
                                         .ToListAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
