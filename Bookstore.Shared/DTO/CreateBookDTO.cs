using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DTO
{
    public class CreateBookDTO
    {
        [Required]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
