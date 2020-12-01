using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Models
{
    public class ShoppingCart
    {
        private ApplicationDbContext storeDB;
        public ShoppingCart()
        {
            storeDB = new ApplicationDbContext();
        }

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Item item,int? quantity)
        {
            var cartItem = storeDB.Carts.Include("Item").SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ItemId == item.ItemId);
            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = item.ItemId,
                    CartId = ShoppingCartId,
                    Count = (int)quantity,
                    DateCreated = DateTime.Now
                };
                var variable = storeDB.Items.Find(item.ItemId);
                variable.Quantity -= quantity ?? 0;
                storeDB.Carts.Add(cartItem);  
            }

            storeDB.SaveChanges();
            
           
        }
        public int RemoveFromCart(int id)
        {

            var cartItem = storeDB.Carts.Include("Item").Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
               
                storeDB.Carts.Remove(cartItem);

                storeDB.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Include("Item").Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {

            int? count = (from cartItems in storeDB.Carts
                         where cartItems.CartId == ShoppingCartId 
                         select (int?)cartItems.Count).Sum();

            return count??0 ;
        }

        public decimal GetTotal()
        {

            decimal? total = (decimal?)(from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Item.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.ItemId,
                    OrderId = order.OrderId,
                    UnitPrice =(decimal)item.Item.Price,
                    Quantity = item.Item.Quantity
                };
                
                orderTotal += (decimal)(item.Item.Quantity * item.Item.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }
            order.Total = orderTotal;
            
            storeDB.SaveChanges();

            EmptyCart();

            return order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {

                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        public void MigrateCart(string Email)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = Email;
            }
            storeDB.SaveChanges();
        }

    }
}