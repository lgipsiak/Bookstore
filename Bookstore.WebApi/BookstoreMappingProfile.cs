using AutoMapper;
using Bookstore.DAL.Entities;
using Bookstore.Shared.DTO;

namespace Bookstore.WebApi
{
    class BookstoreMappingProfile : Profile
    {
        public BookstoreMappingProfile()
        {
            CreateMap<Author, AuthorBookDTO>();

            CreateMap<CreateAuthorDTO, Author>();

            CreateMap<UpdateAuthorDTO, Author>();

            CreateMap<Book, BookAuthorDTO>();

            CreateMap<UpdateBookDTO, Book>();

            CreateMap<CreateBookDTO, Book>();
        }
    }
}
