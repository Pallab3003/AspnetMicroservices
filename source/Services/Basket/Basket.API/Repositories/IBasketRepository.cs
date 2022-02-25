using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShopingCart> GetBasket(string userName);
        Task<ShopingCart> UpdateBasket(ShopingCart basket);
        Task DeleteBasket(string userName);
    }
}
