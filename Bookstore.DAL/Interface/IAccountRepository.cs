using Bookstore.DAL.Entities;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface IAccountRepository
    {
        Task SaveAsync();
        Task RegisterAsync(User user);
        Task<User> GetUserByEmail(string email);
    }
}
