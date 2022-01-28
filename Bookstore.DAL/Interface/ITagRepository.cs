using Bookstore.DAL.Entities;
using System.Threading.Tasks;

namespace Bookstore.DAL.Interface
{
    public interface ITagRepository
    {
        Task SaveAsync();
        Task CreateAsync(Tag tag);
    }
}
