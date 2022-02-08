using Bookstore.BLL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bookstore.WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            await _accountService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _accountService.GenerateJwt(dto);

            return Ok(token);
        }

        [HttpPut("{id}/password")]
        public async Task<ActionResult> ChangePassword([FromRoute] int id,
                                                       [FromBody] ChangePasswordDTO dto)
        {
            await _accountService.ChangePassword(id, dto);

            return Ok();
        }
    }
}
