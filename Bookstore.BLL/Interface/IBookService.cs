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
        Task UpdateBook(int id, UpdateBookDTO dto);
        Task DeleteBook(int id);
    }
}
