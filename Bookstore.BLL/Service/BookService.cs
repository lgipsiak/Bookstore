using AutoMapper;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class BookService : IBookService
    {
        private readonly ILogger _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(ILogger<IBookService> logger, IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateBook(CreateBookDTO dto)
        {
            var book = _mapper.Map<Book>(dto);

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
                throw new NotFoundException("Book not found.");

            return _mapper.Map<BookDTO>(book);
        }

        public async Task UpdateBook(int id, UpdateBookDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            //TODO: Change mapping from manual to automatic.

            book.Title = dto.Title;
            book.ReleaseDate = dto.ReleaseDate;

            await _bookRepository.SaveAsync();
        }

        public async Task DeleteBook(int id)
        {
            _logger.LogError($"Book with Id: {id} DELETE action invoked.");

            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            await _bookRepository.DeleteAsync(book);
        }
    }
}
