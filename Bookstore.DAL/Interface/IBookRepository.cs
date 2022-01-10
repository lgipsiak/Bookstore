using Bookstore.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface IBookRepository
    {
        public Task SaveAsync();
        public Task CreateAsync(Book book);
        public Task<Book> GetByIdAsync(int id);
        public Task<IEnumerable<Book>> GetAllAsync();
        public Task DeleteAsync(Book book);
    }
}
