using ShopDongHo.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopDongHo.Helpers
{
    public static class SessionHelper
    {
        private const string CartSessionKey = "ShoppingCart";

        public static List<CartItemViewModel> GetCart()
        {
            var cart = HttpContext.Current.Session[CartSessionKey] as List<CartItemViewModel>;
            if (cart == null)
            {
                cart = new List<CartItemViewModel>();
                HttpContext.Current.Session[CartSessionKey] = cart;
            }
            return cart;
        }

        public static void AddToCart(CartItemViewModel item)
        {
            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(c => c.ProductId == item.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            HttpContext.Current.Session[CartSessionKey] = cart;
        }

        public static void UpdateCartItem(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }

            HttpContext.Current.Session[CartSessionKey] = cart;
        }

        public static void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Current.Session[CartSessionKey] = cart;
        }

        public static void ClearCart()
        {
            HttpContext.Current.Session[CartSessionKey] = new List<CartItemViewModel>();
        }

        public static int GetCartItemCount()
        {
            return GetCart().Sum(c => c.Quantity);
        }

        public static decimal GetCartTotal()
        {
            return GetCart().Sum(c => c.SubTotal);
        }
    }
}
