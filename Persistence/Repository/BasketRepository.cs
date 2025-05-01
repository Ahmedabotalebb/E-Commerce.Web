using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModels;
using StackExchange.Redis;

namespace Persistence.Repository
{
    class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database =connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var Jsonbasket = JsonSerializer.Serialize(basket);
            bool IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, Jsonbasket, timeToLive ?? TimeSpan.FromDays(30));
            if(IsCreatedOrUpdated) 
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var basket = await _database.StringGetAsync(key);
            if (basket.IsNullOrEmpty) return null;
            else
                return  JsonSerializer.Deserialize<CustomerBasket>(basket!);

            
        }
    }
}
