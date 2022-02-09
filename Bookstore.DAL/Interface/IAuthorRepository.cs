using Bookstore.DAL.Entities;
using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface IAuthorRepository
    {
        Task SaveAsync();
        Task CreateAsync(Author author);
        Task<Author> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync(AuthorQuery query);
        Task DeleteAsync(Author author);
    }
}
