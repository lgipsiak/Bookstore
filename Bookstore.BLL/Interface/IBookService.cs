using Bookstore.Shared.DTO;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IBookService
    {
        Task CreateBook(CreateBookDTO dto);
        Task<BookDTO> GetBookById(int id);
        Task<PagedResult<BookDTO>> GetAllBooks(BookQuery query);
        Task UpdateBook(int id, UpdateBookDTO dto);
        Task DeleteBook(int id);
    }
}
