using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radicitus.Health.Dto.Dto;

namespace Radicitus.Health.Redis.RadicitusRedis
{
    public interface IRadicitusRedisDataRepository
    {
        Task StoreResourceItem(ResourceItem item);
        Task<IEnumerable<ResourceItem>> GetAllResourceItems();
        Task<List<ResourceItem>> GetAllResourceItemsByTags(List<string> tags);
        Task<List<string>> GetAllTags();
        Task AddTags(List<string> tags);
        Task RemoveResource(Guid guid);
        Task StoreStringAsync(string key, string value);
        Task<string> GetStringAsync(string key);
        Task<ResourceItem> GetResourceItemByGuid(Guid guid);
    }
}
