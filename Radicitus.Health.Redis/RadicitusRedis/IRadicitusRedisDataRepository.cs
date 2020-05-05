using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Redis.RadicitusRedis
{
    public interface IRadicitusRedisDataRepository
    {
        Task StoreResourceItem(ResourceItem item);
        Task<IEnumerable<ResourceItem>> GetAllResourceItems();
        Task<Dictionary<string, List<ResourceItem>>> GetAllResourceItemsByTags(List<string> tags);
    }
}
