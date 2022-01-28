using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.DAL.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Message { get; set; }

        public ICollection<BookTag> Book_Tag { get; set; }
    }
}
