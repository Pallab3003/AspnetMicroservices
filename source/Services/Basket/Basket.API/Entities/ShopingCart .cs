using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShopingCart
    {
   

        public string UserName { get; set; }
        public List<ShoppingCartItems> Items { get; set; } = new List<ShoppingCartItems>();

        public ShopingCart()
        {
        }
        public ShopingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get 
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }

            
        }

    }
}
