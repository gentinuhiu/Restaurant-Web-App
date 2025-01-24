using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FoodCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FoodCategories
        public ActionResult Index()
        {
            return View(db.FoodCategories.ToList());
        }
        [AllowAnonymous]

        // GET: FoodCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategory foodCategory = db.FoodCategories.Find(id);
            if (foodCategory == null)
            {
                return HttpNotFound();
            }
            FoodCategoryModel2 model = new FoodCategoryModel2();
            model.Id = foodCategory.Id;
            model.Name = foodCategory.Name;
            model.imageUrl = foodCategory.imageUrl;
            model.Foods = foodCategory.Foods;
            model.Days = db.Days.ToList();
            return View(model);
        }

        // GET: FoodCategories/Create
        public ActionResult Create()
        {
            FoodCategoryModel model = new FoodCategoryModel();
            return View(model);
        }

        // POST: FoodCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,imageFile")] FoodCategoryModel model)
        {
            if (ModelState.IsValid && model.imageFile != null)
            {                    
                string fileName = Path.GetFileName(model.imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                FoodCategory FoodCategory = new FoodCategory();

                model.imageFile.SaveAs(path);
                FoodCategory.Name = model.Name;
                FoodCategory.imageUrl = Url.Content("~/UploadedImages/" + fileName);

                db.FoodCategories.Add(FoodCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: FoodCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategory foodCategory = db.FoodCategories.Find(id);
            if (foodCategory == null)
            {
                return HttpNotFound();
            }

            FoodCategoryModel model = new FoodCategoryModel();
            model.Id = foodCategory.Id;
            model.Name = foodCategory.Name;
            model.imageUrl = foodCategory.imageUrl;

            return View(model);
        }

        // POST: FoodCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,imageFile,imageUrl")] FoodCategoryModel model)
        {
            FoodCategory foodCategory = db.FoodCategories.Find(model.Id);
            if(foodCategory == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                if (model.imageFile != null)
                {
                    string fileName = Path.GetFileName(model.imageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                    model.imageFile.SaveAs(path);
                    foodCategory.imageUrl = Url.Content("~/UploadedImages/" + fileName);
                }
                foodCategory.Name = model.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: FoodCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodCategory foodCategory = db.FoodCategories.Find(id);
            if (foodCategory == null)
            {
                return HttpNotFound();
            }
            return View(foodCategory);
        }

        // POST: FoodCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            FoodCategory foodCategory = db.FoodCategories.Find(id);

            if (foodCategory == null)
                return HttpNotFound();

            List<Food> Foods = foodCategory.Foods;
            db.Foods.RemoveRange(Foods);
            db.FoodCategories.Remove(foodCategory);
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
