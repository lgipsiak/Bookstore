using Bookstore.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface IAuthorRepository
    {
        public Task SaveAsync();
        public Task CreateAsync(Author author);
        public Task<Author> GetByIdAsync(int id);
        public Task<IEnumerable<Author>> GetAllAsync();
        public Task DeleteAsync(Author author);
    }
}
