using ECommerceDomain.Contarcts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Repositires
{
    public class CachRepo : ICachRepo
    {
        private readonly IDatabase _database;
        public CachRepo(IConnectionMultiplexer connection ) {
            _database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            return value.ToString();
        }

        public async Task SetAsync(string Key, string Value, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(Key, Value, TimeToLive);

        }
    }
}
