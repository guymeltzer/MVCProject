using System;
using System.Collections.Generic;
using System.Linq;
using Models.Models;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Repositories
{
    public class ShoppingCart
    {
        public static void AddItemToCart(int id)
        {
            
        }

        public static void RemoveItemFromCart(int id)
        {
            var userCart = HttpContext.Current.Session["Videos"] as List<int> ?? new List<int>();
            var globalCart = HttpContext.Current.Application["Videos"] as List<int> ?? new List<int>();


            userCart.Remove(id);
            globalCart.Add(id);

            HttpContext.Current.Session["Videos"] = userCart;
        }

        public static void ClearShoppingCart()
        {
            var userCart = HttpContext.Current.Session["Videos"] as List<int> ?? new List<int>();
            var globalCart = HttpContext.Current.Application["Videos"] as List<int> ?? new List<int>();

            foreach (var item in userCart)
            {
                globalCart.Remove(item);
            }

            HttpContext.Current.Session.Remove("Videos");
            HttpContext.Current.Application["Videos"] = globalCart;
        }

        public static List<int> GetItemsInCart()
        {
            return HttpContext.Current.Session["Videos"] as List<int> ?? new List<int>();
        }

        public static List<int> GetAllItemsInAllShoppingCarts()
        {
            return HttpContext.Current.Application["Videos"] as List<int> ?? new List<int>();
        }
    }
}
