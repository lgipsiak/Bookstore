using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IAuthorService
    {
        Task CreateAuthor(CreateAuthorDTO dto);
        Task<AuthorBookDTO> GetAuthorById(int id);
        Task<IEnumerable<AuthorBookDTO>> GetAllAuthors();
        Task UpdateAuthor(int id, UpdateAuthorDTO dto);
        Task DeleteAuthor(int id);
    }
}