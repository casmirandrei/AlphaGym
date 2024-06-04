using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using AlphaGym.Models;
using System.Collections.Generic;
using System.Linq;

namespace AlphaGym.Services
{
    public class CartService
    {
        private readonly GymContext _context;
        private readonly ISession _session;

        public CartService(GymContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddToCart(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null) return;

            Dictionary<int, int> cart = _session.GetObjectFromJson<Dictionary<int, int>>("Cart") ?? new Dictionary<int, int>();

            // Check for existing subscription plans and remove them if adding a new subscription
            var subscriptionIds = new List<int> { 1, 2, 3 }; // Assuming these are the IDs for Basic, Premium, and Personal Trainer subscriptions
            if (subscriptionIds.Contains(productId))
            {
                foreach (var id in subscriptionIds)
                {
                    if (cart.ContainsKey(id) && id != productId)
                    {
                        cart.Remove(id);
                    }
                }
            }

            // Add or update the product quantity in the cart
            if (cart.ContainsKey(productId))
            {
                cart[productId]++;
            }
            else
            {
                cart[productId] = 1;
            }

            _session.SetObjectAsJson("Cart", cart);
        }

        public List<(Product Product, int Quantity)> GetCartItems()
        {
            Dictionary<int, int> cart = _session.GetObjectFromJson<Dictionary<int, int>>("Cart") ?? new Dictionary<int, int>();
            var items = new List<(Product Product, int Quantity)>();

            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductID == item.Key);
                if (product != null)
                {
                    items.Add((product, item.Value));
                }
            }

            return items;
        }

        public int GetCartCount()
        {
            Dictionary<int, int> cart = _session.GetObjectFromJson<Dictionary<int, int>>("Cart") ?? new Dictionary<int, int>();
            return cart.Values.Sum();
        }

        public void ClearCart()
        {
            _session.Remove("Cart");
        }

        public decimal GetCartTotal()
        {
            Dictionary<int, int> cart = _session.GetObjectFromJson<Dictionary<int, int>>("Cart") ?? new Dictionary<int, int>();
            decimal total = 0;

            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductID == item.Key);
                if (product != null)
                {
                    total += (decimal)product.UnitPrice * item.Value;
                }
            }

            return total;
        }

    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
