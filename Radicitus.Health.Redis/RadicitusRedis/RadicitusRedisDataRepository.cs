using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Radicitus.Health.Dto.Dto;
using StackExchange.Redis;

namespace Radicitus.Health.Redis.RadicitusRedis
{
    public class RadicitusRedisDataRepository : IRadicitusRedisDataRepository
    {

        private readonly RedisConnection _connection;
        private readonly IDatabase _db;
        public RadicitusRedisDataRepository(RedisConnection connection)
        {
            _connection = connection;
            _db = _connection.Connection.Value.GetDatabase();
        }


        public async Task<IEnumerable<ResourceItem>> GetAllResourceItems()
        {
            var json = await _db.SetMembersAsync("resources");
            var resources = new List<ResourceItem>();
            foreach (var item in json)
            {
                var deserializedResource = JsonSerializer.Deserialize<ResourceItem>(item);
                resources.Add(deserializedResource);
            }
            return resources;
        }

        public async Task<Dictionary<string, List<ResourceItem>>> GetAllResourceItemsByTags(List<string> tags)
        {
            var dict = new Dictionary<string, List<ResourceItem>>();
            foreach (var tag in tags)
            {
                var members = await _db.SetMembersAsync(tag.ToLower());
                foreach (var member in members)
                {
                    var deserializedMember = JsonSerializer.Deserialize<ResourceItem>(member);
                    if (!dict.ContainsKey(tag))
                    {
                        dict.Add(tag, new List<ResourceItem> { deserializedMember });
                    }
                    else
                    {
                        dict[tag].Add(deserializedMember);
                    }
                }
            }
            return dict;
        }

        public async Task StoreResourceItem(ResourceItem item)
        {
            using var memoryStream = new MemoryStream();
            await JsonSerializer.SerializeAsync(memoryStream, item);
            var json = Encoding.UTF8.GetString(memoryStream.ToArray());
            await _db.StringSetAsync(item.Guid.ToString(), json);
            foreach (var tag in item.Tags)
            {
                var lowerCaseTag = tag.ToLower();
                await _db.SetAddAsync(lowerCaseTag, json);
            }
            await _db.SetAddAsync("resources", json);
        }
    }
}
