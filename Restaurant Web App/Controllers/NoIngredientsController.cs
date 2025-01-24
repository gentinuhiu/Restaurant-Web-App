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
    public class NoIngredientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NoIngredients
        public ActionResult Index()
        {
            return View(db.NoIngredients.ToList());
        }
        // GET: NoIngredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] NoIngredient noIngredient)
        {
            if (ModelState.IsValid)
            {
                db.NoIngredients.Add(noIngredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noIngredient);
        }

        // GET: NoIngredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoIngredient noIngredient = db.NoIngredients.Find(id);
            if (noIngredient == null)
            {
                return HttpNotFound();
            }
            return View(noIngredient);
        }

        // POST: NoIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] NoIngredient noIngredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noIngredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noIngredient);
        }

        // GET: NoIngredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoIngredient noIngredient = db.NoIngredients.Find(id);
            if (noIngredient == null)
            {
                return HttpNotFound();
            }
            return View(noIngredient);
        }

        // POST: NoIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            NoIngredient noIngredient = db.NoIngredients.Find(id);

            if (noIngredient == null)
                return HttpNotFound();

            db.NoIngredients.Remove(noIngredient);
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
