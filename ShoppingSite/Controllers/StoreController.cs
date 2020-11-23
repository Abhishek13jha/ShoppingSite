using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingSite.Models;
using ShoppingSite.ViewModels;

namespace ShoppingSite.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        private ApplicationDbContext _context;
        public StoreController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        
        public ActionResult Browse(string category)
        {
            var categoryModel = _context.Categories.Include("Items").Single(c => c.CategoryName == category);
            return View(categoryModel);
            
        }
        public ActionResult Details(int id)
        {
            var item = _context.Items.Include("Producer").Where(s => s.ItemId == id).FirstOrDefault<Item>();
            return View(item);
        }

        
    }
}