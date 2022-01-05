using AutoMapper;
using Bookstore.DAL.Entities;
using Bookstore.Shared.DTO;

namespace Bookstore.BLL.Service
{
    class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<UpdateBookDTO, Book>();
            CreateMap<CreateBookDTO, Book>();
        }
    }
}
