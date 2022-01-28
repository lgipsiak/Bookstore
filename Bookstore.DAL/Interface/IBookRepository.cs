using Bookstore.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface IBookRepository
    {
        Task SaveAsync();
        Task CreateAsync(Book book);
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task DeleteAsync(Book book);
    }
}
