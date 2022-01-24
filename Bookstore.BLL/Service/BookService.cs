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
            _logger = logger;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task CreateBook(CreateBookDTO dto)
        {
            var book = new Book()
            {
                Title = dto.Title,
                ReleaseDate = dto.ReleaseDate
            };

            var bookAuthors = new List<BookAuthor>();

            foreach (var authorId in dto.AuthorIds)
            {
                var bookAuthor = new BookAuthor
                {
                    AuthorId = authorId,
                    Book = book
                };
                bookAuthors.Add(bookAuthor);
            }
            book.Book_Author = bookAuthors;

            await _bookRepository.CreateAsync(book);
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();

            var bookDTOs = _mapper.Map<List<BookDTO>>(books);

            return bookDTOs;
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            var authorDTOs = new List<AuthorDTO>();

            foreach (var author in book.Book_Author)
            {
                var authorDTO = new AuthorDTO
                {
                    Id = author.Author.Id,
                    FirstName = author.Author.FirstName,
                    LastName = author.Author.LastName,
                    Description = author.Author.Description
                };
                authorDTOs.Add(authorDTO);
            }

            var bookDTO = _mapper.Map<BookDTO>(book);

            bookDTO.AuthorDTOs = authorDTOs;

            return bookDTO;
        }

        public async Task UpdateBook(int id, UpdateBookDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            book.Title = dto.Title;
            book.ReleaseDate = dto.ReleaseDate;

            await _bookRepository.SaveAsync();
        }

        public async Task DeleteBook(int id)
        {
            _logger.LogWarning($"Book with Id: {id} DELETE action invoked.");

            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            await _bookRepository.DeleteAsync(book);
        }
    }
}
