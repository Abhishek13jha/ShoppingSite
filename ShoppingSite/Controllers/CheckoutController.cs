﻿using ShoppingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext storeDB;
        public CheckoutController()
        {
            storeDB = new ApplicationDbContext();
        }
      
        const string PromoCode = "50";

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;


                    storeDB.Orders.Add(order);

                    storeDB.SaveChanges();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {

                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {

            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.UserName == User.Identity.Name);
            var val = storeDB.Orders.Find(id);
            if (isValid)
            {
                return View(val);
            }
            else
            {
                return View("Error");
            }
        }
    }
}