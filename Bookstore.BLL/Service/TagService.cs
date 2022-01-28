using Bookstore.BLL.Interface;
using Bookstore.DAL.Entities;
using Bookstore.DAL.Interface;
using Bookstore.Shared.DTO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Bookstore.BLL.Service
{
    public class TagService : ITagService
    {
        private readonly ILogger<ITagService> _logger;
        private readonly ITagRepository _tagRepository;

        public TagService(ILogger<ITagService> logger, ITagRepository tagRepository)
        {
            _logger = logger;
            _tagRepository = tagRepository;
        }

        public async Task CreateTag(CreateTagDTO dto)
        {
            var tag = new Tag()
            {
                Message = dto.Message
            };

            await _tagRepository.CreateAsync(tag);
        }
    }
}
