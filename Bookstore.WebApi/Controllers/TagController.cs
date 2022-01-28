using Bookstore.BLL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.WebApi.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTagDTO dto)
        {
            await _tagService.CreateTag(dto);

            return Ok();
        }
    }
}
