using Bookstore.Shared.DTO;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IAuthorService
    {
        Task CreateAuthor(CreateAuthorDTO dto);
        Task<AuthorDTO> GetAuthorById(int id);
        Task<PagedResult<AuthorDTO>> GetAllAuthors(AuthorQuery query);
        Task UpdateAuthor(int id, UpdateAuthorDTO dto);
        Task DeleteAuthor(int id);
    }
}