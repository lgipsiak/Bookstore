using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<BookAuthor> Book_Author { get; set; }
    }
}
