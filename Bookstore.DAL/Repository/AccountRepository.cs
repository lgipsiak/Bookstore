using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bookstore.DAL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BookstoreDbContext _dbContext;

        public AccountRepository(BookstoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task RegisterAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Include(x => x.Role)
                                         .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
