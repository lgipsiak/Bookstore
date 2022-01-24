﻿using AutoMapper;
using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        public async Task<AuthorDTO> GetAuthorById(int id)
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

            var authorDTO = _mapper.Map<AuthorDTO>(author);

            authorDTO.BookDTOs = bookDTOs;

            return authorDTO;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();

            return _mapper.Map<List<AuthorDTO>>(authors);
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
