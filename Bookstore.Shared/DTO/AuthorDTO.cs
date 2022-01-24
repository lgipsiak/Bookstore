using System.Collections.Generic;

namespace Bookstore.Shared.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<BookDTO> BookDTOs { get; set; }
    }
}
