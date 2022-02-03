using Bookstore.Shared.DTO;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDTO dto);
        Task<string> GenerateJwt(LoginDTO dto);
    }
}
