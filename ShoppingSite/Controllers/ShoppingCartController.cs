using ShoppingSite.Models;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;
        public ShoppingCartController()
        {
            _context = new ApplicationDbContext();
        }
        /*protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }*/

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal =cart.GetTotal()

            };

            return View(viewModel);
        }
        
        public ActionResult AddToCart(int Id,int? Quantity)
        {
            if (Quantity ==null)
            {
                return View("AddToCartSecond");
            }
            else
            {

                var addedItem = _context.Items
                    .SingleOrDefault(item => item.ItemId == Id);

                var cart = ShoppingCart.GetCart(this.HttpContext);

                cart.AddToCart(addedItem, Quantity);


                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {

            var cart = ShoppingCart.GetCart(this.HttpContext);
            string itemName = _context.Carts
                .Single(item => item.RecordId == id).Item.ItemName;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}