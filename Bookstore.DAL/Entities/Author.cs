using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Description { get; set; }
        public ICollection<BookAuthor> Book_Author { get; set; }
    }
}
