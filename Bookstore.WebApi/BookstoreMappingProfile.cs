using AutoMapper;
using Bookstore.DAL.Entities;
using Bookstore.Shared.DTO;

namespace Bookstore.WebApi
{
    class BookstoreMappingProfile : Profile
    {
        public BookstoreMappingProfile()
        {
            CreateMap<Author, AuthorDTO>();

            CreateMap<CreateAuthorDTO, Author>();

            CreateMap<UpdateAuthorDTO, Author>();

            CreateMap<Book, BookDTO>();

            CreateMap<UpdateBookDTO, Book>();

            CreateMap<CreateBookDTO, Book>();
        }
    }
}
