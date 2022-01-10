using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DTO
{
    public class CreateAuthorDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}