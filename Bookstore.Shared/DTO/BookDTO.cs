using System;
using System.Collections.Generic;

namespace Bookstore.Shared.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<AuthorBookDTO> AuthorDTOs { get; set; }
        public List<TagDTO> TagDTOs { get; set; }
    }
}
