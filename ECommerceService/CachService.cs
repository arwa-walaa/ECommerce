using ECommerceDomain.Contarcts;
using ECommerceServiceApstarction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceService
{
    public class CachService : ICachService
    {
        private readonly ICachRepo _cachRepo;

        public CachService(ICachRepo cachRepo)
        {
            _cachRepo = cachRepo;
        }
        public async Task<string?> GetAsync(string key)
        {
            return await _cachRepo.GetAsync(key);
        }

        public async Task SetAsync(string Key, object Value, TimeSpan TimeToLive)
        {
            await _cachRepo.SetAsync(Key, System.Text.Json.JsonSerializer.Serialize(Value), TimeToLive);
        }
    }
}
