using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IBookService
    {
        Task<BookDTO> GetBookById(int id);
        Task<IEnumerable<BookDTO>> GetAllBooks();
        Task CreateBook(CreateBookDTO dto);
        Task<bool> UpdateBook(int id, UpdateBookDTO dto);
        Task<bool> DeleteBook(int id);
    }
}
