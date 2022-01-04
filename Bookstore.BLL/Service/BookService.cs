using AutoMapper;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task CreateBook(CreateBookDTO dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate
            };

            await _bookRepository.CreateAsync(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return null;

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<bool> UpdateBook(int id, UpdateBookDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return false;

            book.Title = dto.Title;
            book.ReleaseDate = dto.ReleaseDate;

            await _bookRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                return false;

            await _bookRepository.DeleteAsync(book);
            return true;
        }
    }
}
