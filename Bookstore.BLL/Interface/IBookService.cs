using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IBookService
    {
        Task CreateBook(CreateBookDTO dto);
        Task<BookDTO> GetBookById(int id);
        Task<IEnumerable<BookDTO>> GetAllBooks();
        Task UpdateBook(int id, UpdateBookDTO dto);
        Task DeleteBook(int id);
    }
}
