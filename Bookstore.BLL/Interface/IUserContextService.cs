using System.Security.Claims;

namespace Bookstore.BLL.Interface
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }
}