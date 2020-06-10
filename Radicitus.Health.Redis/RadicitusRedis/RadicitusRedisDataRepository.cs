using System;
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
            var guids = (await _db.SetMembersAsync("resources")).Select(x => new RedisKey(x)).ToArray();
            var resources = new List<ResourceItem>();
            var jsonList = await _db.StringGetAsync(guids);
            foreach (var item in jsonList)
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
            var fetchedTags = await _db.SetCombineAsync(SetOperation.Intersect, redisKeys);
            if (!fetchedTags.Any())
            {
                fetchedTags = await _db.SetCombineAsync(SetOperation.Union, redisKeys);
            }
            var list = await _db.StringGetAsync(fetchedTags.Select(x => new RedisKey(x)).ToArray());
            foreach (var item in list)
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

        public async Task<ResourceItem> GetResourceItemByGuid(Guid guid)
        {
            var json = await _db.StringGetAsync(guid.ToString());
            return JsonSerializer.Deserialize<ResourceItem>(json);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task RemoveResource(Guid guid)
        {
            await _db.SetRemoveAsync("resources", guid.ToString());
            await _db.KeyDeleteAsync(guid.ToString());
            var tags = await _db.SetMembersAsync("tags");
            var tasks = new List<Task>();
            foreach (var tag in tags)
            {
                tasks.Add(_db.SetRemoveAsync(tag.ToString(), guid.ToString()));
            }
            await Task.WhenAll(tasks);
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
                await _db.SetAddAsync(lowerCaseTag, item.Guid.ToString());
                await _db.SetAddAsync("tags", lowerCaseTag);
            }
            await _db.SetAddAsync("resources", item.Guid.ToString());
        }

        public async Task StoreStringAsync(string key, string value, TimeSpan? expiration = null)
        {
            if (expiration != null)
            {
                await _db.StringSetAsync(key, value, expiration);
            }
            else
            {
                await _db.StringSetAsync(key, value);
            }
        }


    }
}
