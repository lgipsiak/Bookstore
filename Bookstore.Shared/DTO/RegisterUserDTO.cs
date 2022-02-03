using System;

namespace Bookstore.Shared.DTO
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPasword { get; set; }

        public int RoleId { get; set; } = 2;
    }
}
