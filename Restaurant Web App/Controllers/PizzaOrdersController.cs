using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PizzaOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PizzaOrders
        public ActionResult Index()
        {
            var pizzaOrders = db.PizzaOrders.Include(p => p.Pizza);
            return View(pizzaOrders.ToList());
        }

        // GET: PizzaOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaOrder pizzaOrder = db.PizzaOrders.Find(id);
            pizzaOrder.Pizza = db.Foods.Find(pizzaOrder.PizzaId);
            if (pizzaOrder == null)
            {
                return HttpNotFound();
            }
            return View(pizzaOrder);
        }

        // GET: PizzaOrders/Create
        public ActionResult Create()
        {
            ViewBag.PizzaId = new SelectList(db.Foods, "Id", "Name");
            return View();
        }

        // POST: PizzaOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PizzaId,SelectedSize")] PizzaOrder pizzaOrder)
        {
            if (ModelState.IsValid)
            {
                db.PizzaOrders.Add(pizzaOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PizzaId = new SelectList(db.Foods, "Id", "Name", pizzaOrder.PizzaId);
            return View(pizzaOrder);
        }

        // GET: PizzaOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaOrder pizzaOrder = db.PizzaOrders.Find(id);
            if (pizzaOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.PizzaId = new SelectList(db.Foods, "Id", "Name", pizzaOrder.PizzaId);
            return View(pizzaOrder);
        }       

        // POST: PizzaOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PizzaId,SelectedSize")] PizzaOrder pizzaOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizzaOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PizzaId = new SelectList(db.Foods, "Id", "Name", pizzaOrder.PizzaId);
            return View(pizzaOrder);
        }

        // GET: PizzaOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PizzaOrder pizzaOrder = db.PizzaOrders.Find(id);
            if (pizzaOrder == null)
            {
                return HttpNotFound();
            }
            return View(pizzaOrder);
        }

        // POST: PizzaOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PizzaOrder pizzaOrder = db.PizzaOrders.Find(id);
            db.PizzaOrders.Remove(pizzaOrder);
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
