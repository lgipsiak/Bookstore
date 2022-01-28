using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using System.Threading.Tasks;

namespace Bookstore.DAL.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public TagRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateAsync(Tag tag)
        {
            await _dbContext.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
        }

    }
}
