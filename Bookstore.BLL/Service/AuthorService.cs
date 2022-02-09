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
    public class AuthorService : IAuthorService
    {
        private readonly ILogger _logger;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(ILogger<IAuthorService> logger,
                             IAuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }
        public async Task CreateAuthor(CreateAuthorDTO dto)
        {
            var author = new Author()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Description = dto.Description
            };

            await _authorRepository.CreateAsync(author);
        }

        public async Task<AuthorDTO> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                throw new NotFoundException("Author not found.");

            var authorDTO = new AuthorDTO()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description,
                BookDTOs = author.Book_Author.Select(book => new BookAuthorDTO()
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    ReleaseDate = book.Book.ReleaseDate,
                    TagDTOs = book.Book.Book_Tag.Select(tag => new TagDTO()
                    {
                        Message = tag.Tag.Message
                    }).ToList()
                }).ToList()
            };

            return authorDTO;
        }

        public async Task<PagedResult<AuthorDTO>> GetAllAuthors(AuthorQuery query)
        {
            var authors = await _authorRepository.GetAllAsync(query);

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Author, object>>>
                {
                    {nameof(Author.FirstName), x => x.FirstName },
                    {nameof(Author.LastName), x => x.LastName }
                };

                var selectedColumn = columnsSelector[query.SortBy];

                authors = query.SortDirection == SortDirection.Ascending
                    ? authors.AsQueryable().OrderBy(selectedColumn)
                    : authors.AsQueryable().OrderByDescending(selectedColumn);
            }
            else
            {
                authors = query.SortDirection == SortDirection.Ascending
                    ? authors.AsQueryable().OrderBy(x => x.Id)
                    : authors.AsQueryable().OrderByDescending(x => x.Id);
            }

            var filteredAuthors = authors.Skip(query.PageSize * (query.PageNumber - 1))
                                         .Take(query.PageSize);

            var authorDTOs = filteredAuthors.Select(author => new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description,
                BookDTOs = author.Book_Author.Select(book => new BookAuthorDTO
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    ReleaseDate = book.Book.ReleaseDate,
                    TagDTOs = book.Book.Book_Tag.Select(tag => new TagDTO()
                    {
                        Message = tag.Tag.Message
                    }).ToList()
                }).ToList()
            }).ToList();

            var pagedResult = new PagedResult<AuthorDTO>(authorDTOs,
                                              authors.Count(),
                                              query.PageSize,
                                              query.PageNumber);

            return pagedResult;
        }

        public async Task UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                throw new NotFoundException("Author not found.");

            if (dto.FirstName is not null)
                author.FirstName = dto.FirstName;

            if (dto.LastName is not null)
                author.LastName = dto.LastName;

            if (dto.Description is not null)
                author.Description = dto.Description;

            await _authorRepository.SaveAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            _logger.LogWarning($"Author with Id: {id} DELETE action invoked.");

            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                throw new NotFoundException("Author not found.");

            await _authorRepository.DeleteAsync(author);
        }
    }
}
