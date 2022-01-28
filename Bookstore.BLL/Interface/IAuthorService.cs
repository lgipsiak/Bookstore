using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IAuthorService
    {
        Task CreateAuthor(CreateAuthorDTO dto);
        Task<AuthorDTO> GetAuthorById(int id);
        Task<IEnumerable<AuthorDTO>> GetAllAuthors();
        Task UpdateAuthor(int id, UpdateAuthorDTO dto);
        Task DeleteAuthor(int id);
    }
}