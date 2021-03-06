﻿using ShoppingSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string tags)
        {
            var item = _context.Items.Where(x => x.ItemName.StartsWith(tags)).FirstOrDefault();
            return View(item);
        }

        public JsonResult getName()
        {
            var list_item = _context.Items.OrderBy(x => x.ItemId).ToList();
            return Json(list_item, JsonRequestBehavior.AllowGet);
        }
       // ["home"/"About"]
        public ActionResult About()
        {

            return View("AdminView");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}