using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommercePersistence.Repositires
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;
        //connection from redis =>IConnectionMultiplexer
        public BasketRepo(IConnectionMultiplexer connection) {
        
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default) 
        {
           var IsCreatedOrUpdated=await _database.StringSetAsync(basket.Id, System.Text.Json.JsonSerializer.Serialize(basket),(timeToLive==default)? TimeSpan.FromDays(7) : timeToLive );
            if (IsCreatedOrUpdated)
            {
                var Basket = await _database.StringGetAsync(basket.Id);

                return JsonSerializer.Deserialize<CustomerBasket>(Basket);

            }
            else { 
            
                return null!;
            }


        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket =await _database.StringGetAsync(basketId);
            if (Basket.IsNullOrEmpty)
            {
                return null;
            }
            else
            {
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
            }


        }
    }
}
