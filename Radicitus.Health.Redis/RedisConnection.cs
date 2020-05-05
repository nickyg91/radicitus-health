using System;
using StackExchange.Redis;

namespace Radicitus.Health.Redis
{
    public class RedisConnection : IRedisRepository
    {
        public Lazy<ConnectionMultiplexer> Connection;
        private readonly string _connectionString;
        public RedisConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            Connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_connectionString));
        }
    }
}
