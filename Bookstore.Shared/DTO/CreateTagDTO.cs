using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DTO
{
    public class CreateTagDTO
    {
        [Required]
        public string Message { get; set; }
    }
}
