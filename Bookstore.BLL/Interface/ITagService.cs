using Bookstore.Shared.DTO;
using System.Threading.Tasks;

namespace Bookstore.BLL.Interface
{
    public interface ITagService
    {
        Task CreateTag(CreateTagDTO dto);
    }
}
