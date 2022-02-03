using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Shared.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public string ConfirmPasword { get; set; }

        public int RoleId { get; set; } = 2;
    }
}
