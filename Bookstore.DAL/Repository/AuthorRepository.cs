using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return await _dbContext.Authors.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task DeleteAsync(Author author)
        {
            _dbContext.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
    }
}