using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.repository
{
    public class BasketRepository : IBasketRepository
    {
        // دي مش تبع الredis 
        // هي مسؤله انها تفتح اتصال مع داتا بيز مش بتاعتنا 
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis) // Ask CLR To Create Obj From Redis  
        {
            _database = redis.GetDatabase();

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
         => await _database.KeyDeleteAsync(basketId);


        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var customerBasket = await _database.StringGetAsync(basketId);
            return customerBasket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(customerBasket.ToString());
        }

        public async Task<CustomerBasket?> UpdateOrCreateBasketAsync( CustomerBasket basket)
        {

            var result = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket));
            if (result) return  await GetBasketAsync(basket.Id);
            return null;

        }



    }
}
