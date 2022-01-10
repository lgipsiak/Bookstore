using Bookstore.BLL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.WebApi.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateAuthorDTO dto)
        {
            await _authorService.CreateAuthor(dto);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            return Ok(author);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAuthors();

            return Ok(authors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id,
                                               [FromBody] UpdateAuthorDTO dto)
        {
            await _authorService.UpdateAuthor(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _authorService.DeleteAuthor(id);

            return NoContent();
        }
    }
}