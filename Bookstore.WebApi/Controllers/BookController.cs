using Bookstore.BLL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.WebApi.Controllers
{
    [Route("api/book")]
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _bookService.CreateBook(dto);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooks();

            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id,
                                               [FromBody] UpdateBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = await _bookService.UpdateBook(id, dto);

            if (isUpdated == false)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _bookService.DeleteBook(id);

            if (isDeleted == false)
                return NotFound();

            return Ok();
        }
    }
}
