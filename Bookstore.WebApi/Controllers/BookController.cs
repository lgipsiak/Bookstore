using Bookstore.BLL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.WebApi.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateBookDTO dto)
        {
            await _bookService.CreateBook(dto);

            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookById(id);

            return Ok(book);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooks();

            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id,
                                               [FromBody] UpdateBookDTO dto)
        {
            await _bookService.UpdateBook(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _bookService.DeleteBook(id);

            return NoContent();
        }
    }
}