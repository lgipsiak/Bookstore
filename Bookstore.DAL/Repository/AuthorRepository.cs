using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.DAL.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public AuthorRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(Author author)
        {
            await _dbContext.AddAsync(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _dbContext.Authors.Include(x => x.Book_Author)
                                           .ThenInclude(x => x.Book)
                                           .ThenInclude(x => x.Book_Tag)
                                           .ThenInclude(x => x.Tag)
                                           .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAllAsync(AuthorQuery query)
        {
            return await _dbContext.Authors.Include(x => x.Book_Author)
                                           .ThenInclude(x => x.Book)
                                           .ThenInclude(x => x.Book_Tag)
                                           .ThenInclude(x => x.Tag)
                                           .Where(x => query.SearchPhrase == null
                                                       || (x.FirstName.ToLower().Contains(query.SearchPhrase.ToLower()))
                                                       || (x.LastName.ToLower().Contains(query.SearchPhrase.ToLower())))
                                           .ToListAsync();

        }

        public async Task DeleteAsync(Author author)
        {
            _dbContext.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
    }
}