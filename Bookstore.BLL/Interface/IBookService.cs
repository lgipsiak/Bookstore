﻿using Bookstore.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IBookService
    {
        Task CreateBook(CreateBookDTO dto);
        Task<BookAuthorDTO> GetBookById(int id);
        Task<IEnumerable<BookAuthorDTO>> GetAllBooks();
        Task UpdateBook(int id, UpdateBookDTO dto);
        Task DeleteBook(int id);
    }
}
