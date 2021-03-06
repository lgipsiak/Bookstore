using Bookstore.BLL.Exceptions;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class BookService : IBookService
    {
        private readonly ILogger _logger;
        private readonly IBookRepository _bookRepository;

        public BookService(ILogger<IBookService> logger,
                           IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
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

            var bookTags = new List<BookTag>();

            foreach (var tagId in dto.TagIds)
            {
                var bookTag = new BookTag
                {
                    TagId = tagId,
                    Book = book
                };
                bookTags.Add(bookTag);
            }

            book.Book_Author = bookAuthors;

            book.Book_Tag = bookTags;

            await _bookRepository.CreateAsync(book);
        }

        public async Task<PagedResult<BookDTO>> GetAllBooks(BookQuery query)
        {
            var books = await _bookRepository.GetAllAsync(query);

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Book, object>>>
                {
                    {nameof(Book.Title), x => x.Title },
                    {nameof(Book.ReleaseDate), x => x.ReleaseDate }
                };

                var selectedColumn = columnsSelector[query.SortBy];

                books = query.SortDirection == SortDirection.Ascending
                    ? books.AsQueryable().OrderBy(selectedColumn)
                    : books.AsQueryable().OrderByDescending(selectedColumn);
            }
            else
            {
                books = query.SortDirection == SortDirection.Ascending
                    ? books.AsQueryable().OrderBy(x => x.Id)
                    : books.AsQueryable().OrderByDescending(x => x.Id);
            }

            var filteredBooks = books.Skip(query.PageSize * (query.PageNumber - 1))
                                     .Take(query.PageSize);

            var bookDTOs = filteredBooks.Select(book => new BookDTO
            {
                Id = book.Id,
                Title = book.Title,
                ReleaseDate = book.ReleaseDate,
                TagDTOs = book.Book_Tag.Select(tag => new TagDTO
                {
                    Message = tag.Tag.Message
                }).ToList(),
                AuthorDTOs = book.Book_Author.Select(author => new AuthorBookDTO
                {
                    Id = author.Author.Id,
                    FirstName = author.Author.FirstName,
                    LastName = author.Author.LastName,
                    Description = author.Author.Description
                }).ToList()
            }).ToList();

            var pagedResult = new PagedResult<BookDTO>(bookDTOs,
                                                       books.Count(),
                                                       query.PageSize,
                                                       query.PageNumber);

            return pagedResult;
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            var bookDTO = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                ReleaseDate = book.ReleaseDate,
                AuthorDTOs = book.Book_Author.Select(author => new AuthorBookDTO
                {
                    Id = author.Author.Id,
                    FirstName = author.Author.FirstName,
                    LastName = author.Author.LastName,
                    Description = author.Author.Description
                }).ToList(),
                TagDTOs = book.Book_Tag.Select(tag => new TagDTO
                {
                    Message = tag.Tag.Message
                }).ToList()
            };

            return bookDTO;
        }

        public async Task UpdateBook(int id, UpdateBookDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book is null)
                throw new NotFoundException("Book not found.");

            if (dto.Title is not null)
                book.Title = dto.Title;

            if (dto.ReleaseDate is not null)
                book.ReleaseDate = dto.ReleaseDate;

            if (dto.AuthorIds is not null)
            {
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
            }

            if (dto.TagIds is not null)
            {
                var bookTags = new List<BookTag>();

                foreach (var tagId in dto.TagIds)
                {
                    var bookTag = new BookTag
                    {
                        TagId = tagId,
                        Book = book
                    };
                    bookTags.Add(bookTag);
                }
                book.Book_Tag = bookTags;
            }

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
