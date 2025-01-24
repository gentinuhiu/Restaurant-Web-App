using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FoodOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FoodOrders
        public ActionResult Index()
        {
            var foodOrders = db.FoodOrders.Include(f => f.Food);
            return View(foodOrders.ToList());
        }

        // GET: FoodOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }

        // GET: FoodOrders/Create
        public ActionResult Create()
        {
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name");
            return View();
        }

        // POST: FoodOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FoodId,SelectedSize,FoodPrice")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.FoodOrders.Add(foodOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", foodOrder.FoodId);
            return View(foodOrder);
        }

        // GET: FoodOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", foodOrder.FoodId);
            return View(foodOrder);
        }

        // POST: FoodOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FoodId,SelectedSize,FoodPrice")] FoodOrder foodOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodId = new SelectList(db.Foods, "Id", "Name", foodOrder.FoodId);
            return View(foodOrder);
        }

        // GET: FoodOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            if (foodOrder == null)
            {
                return HttpNotFound();
            }
            return View(foodOrder);
        }

        // POST: FoodOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodOrder foodOrder = db.FoodOrders.Find(id);
            db.FoodOrders.Remove(foodOrder);
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
