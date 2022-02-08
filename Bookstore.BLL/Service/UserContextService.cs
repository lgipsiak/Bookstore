using Bookstore.BLL.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bookstore.BLL.Service
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserContextService(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public ClaimsPrincipal User => _httpContextAccesor.HttpContext?.User;
        public int? GetUserId =>
            User is null ? null : int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}
