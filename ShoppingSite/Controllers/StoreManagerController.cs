using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingSite.Models;

namespace ShoppingSite.Controllers
{
    [Authorize(Users ="Abhishek@gmail.com")]
    public class StoreManagerController : Controller
    {
        private ApplicationDbContext db;

        public StoreManagerController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Category).Include(i => i.Producer);
            return View(items.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = db.Items.Include(i => i.Category).Include(i => i.Producer).Where(s => s.ItemId == id).FirstOrDefault<Item>();
            // Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Categories, "categoryId", "CategoryName");
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.Categories, "categoryId", "CategoryName", item.categoryId);
            
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {

                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Categories, "categoryId", "CategoryName", item.categoryId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.Categories, "categoryId", "CategoryName", item.categoryId);
             ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "Name", item.ProducerId);
            return View(item);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
