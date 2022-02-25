using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redcisCache;

        public BasketRepository(IDistributedCache redcisCache)
        {
            _redcisCache = redcisCache;
        }

        public async Task DeleteBasket(string userName)
        {
            await _redcisCache.RemoveAsync(userName);            
        }

        public async Task<ShopingCart> GetBasket(string userName)
        {
            var basket= await _redcisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShopingCart>(basket);
        }

        public async Task<ShopingCart> UpdateBasket(ShopingCart basket)
        {
            await _redcisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }
    }
}
