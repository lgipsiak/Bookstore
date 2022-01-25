﻿using AutoMapper;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(ILogger<IAuthorService> logger, IMapper mapper, IAuthorRepository authorRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }
        public async Task CreateAuthor(CreateAuthorDTO dto)
        {
            var author = _mapper.Map<Author>(dto);

            await _authorRepository.CreateAsync(author);
        }

        public async Task<AuthorBookDTO> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                throw new NotFoundException("Author not found.");

            var bookDTOs = new List<BookDTO>();

            foreach (var book in author.Book_Author)
            {
                var bookDTO = new BookDTO
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    ReleaseDate = book.Book.ReleaseDate
                };
                bookDTOs.Add(bookDTO);
            }

            //var authorDTO = _mapper.Map<AuthorBookDTO>(author);

            var authorDTO = new AuthorBookDTO()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description,
                BookDTOs = bookDTOs
            };

            return authorDTO;
        }

        public async Task<IEnumerable<AuthorBookDTO>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();

            var authorDTOs = authors.Select(author => new AuthorBookDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Description = author.Description,
                BookDTOs = author.Book_Author.Select(book => new BookDTO
                {
                    Id = book.Book.Id,
                    Title = book.Book.Title,
                    ReleaseDate = book.Book.ReleaseDate

                }).ToList()
            }).ToList();

            return authorDTOs;
        }

        public async Task UpdateAuthor(int id, UpdateAuthorDTO dto)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author is null)
                throw new NotFoundException("Author not found.");

            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
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
