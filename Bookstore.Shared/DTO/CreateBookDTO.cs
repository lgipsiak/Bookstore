using System;
using System.Collections.Generic;

namespace Bookstore.Shared.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<int> AuthorIds { get; set; }
        public List<int> TagIds { get; set; }
    }
}
