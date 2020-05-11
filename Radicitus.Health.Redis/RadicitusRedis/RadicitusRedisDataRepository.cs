using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task AddTags(List<string> tags)
        {
            var keyExists = await _db.KeyExistsAsync("tags");
            if (!keyExists)
            {
                var vals = tags.Select(x => new RedisValue(x)).ToArray();
                await _db.SetAddAsync("tags", vals);
            }
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

        public async Task<List<ResourceItem>> GetAllResourceItemsByTags(List<string> tags)
        {
            var items = new List<ResourceItem>();
            var redisKeys = tags.Select(x => new RedisKey(x)).ToArray();
            var intersectingTags = await _db.SetCombineAsync(SetOperation.Intersect, redisKeys);
            foreach (var item in intersectingTags)
            {
                var deserializedMember = JsonSerializer.Deserialize<ResourceItem>(item);
                items.Add(deserializedMember);
            }
            return items;
        }

        public async Task<List<string>> GetAllTags()
        {
            var tags = await _db.SetMembersAsync("tags");
            return tags.Select(x => x.ToString()).ToList();
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
                await _db.SetAddAsync("tags", lowerCaseTag);
            }
            await _db.SetAddAsync("resources", json);
        }


    }
}
