using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    public class FoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Foods.ToList());
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            FoodCategory foodCategory = db.FoodCategories.Find(id);

            if (foodCategory == null)
                return HttpNotFound();

            FoodModel model = new FoodModel();
            model.FoodCategoryId = foodCategory.Id;
            model.FoodCategoryName = foodCategory.Name;
            model.Ingredients = db.Ingredients.ToList();
            model.NoIngredients = db.NoIngredients.ToList();
            model.Options = db.Options.ToList();

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateIndex()
        {
            FoodModel model = new FoodModel();
            model.FoodCategories = db.FoodCategories.ToList();
            model.Ingredients = db.Ingredients.ToList();
            model.NoIngredients = db.NoIngredients.ToList();
            model.Options = db.Options.ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateIndex([Bind(Include = "FoodCategoryId,Name,Description,NormalSizedPrice,SmallSizedPrice,FamilySizedPrice,imageFile,SelectedIngredients,SelectedNoIngredients,SelectedOptions")] FoodModel model)
        {
            Food food = new Food();
            food.Ingredients = new List<Ingredient>();
            food.NoIngredients = new List<NoIngredient>();
            food.Options = new List<Option>();

            if (ModelState.IsValid && model.imageFile != null)
            {
                food.Name = model.Name;
                food.Description = model.Description;
                food.NormalSizedPrice = model.NormalSizedPrice;
                food.SmallSizedPrice = model.SmallSizedPrice;
                food.FamilySizedPrice = model.FamilySizedPrice;

                if (model.SmallSizedPrice == 0 && model.FamilySizedPrice == 0)
                    food.disabledPrices = true;

                if (model.SelectedIngredients == null)
                {
                    model.SelectedIngredients = new List<int>();
                }

                if (model.SelectedNoIngredients == null)
                {
                    model.SelectedNoIngredients = new List<int>();
                }

                if (model.SelectedOptions == null)
                {
                    model.SelectedOptions = new List<int>();
                }

                foreach (int id in model.SelectedIngredients)
                {
                    Ingredient ingredient = db.Ingredients.Find(id);
                    if (ingredient == null)
                        return HttpNotFound();

                    food.Ingredients.Add(ingredient);
                }

                foreach (int id in model.SelectedNoIngredients)
                {
                    NoIngredient noIngredient = db.NoIngredients.Find(id);
                    if (noIngredient == null)
                        return HttpNotFound();

                    food.NoIngredients.Add(noIngredient);
                }

                foreach (int id in model.SelectedOptions)
                {
                    Option option = db.Options.Find(id);
                    if (option == null)
                        return HttpNotFound();

                    food.Options.Add(option);
                }

                string fileName = Path.GetFileName(model.imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                model.imageFile.SaveAs(path);
                food.ImageUrl = Url.Content("~/UploadedImages/" + fileName);

                var FoodCategory = db.FoodCategories.Find(model.FoodCategoryId);

                if (FoodCategory == null)
                    return HttpNotFound();

                FoodCategory.Foods.Add(food);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.FoodCategories = db.FoodCategories.ToList();
            model.Ingredients = db.Ingredients.ToList();
            model.NoIngredients = db.NoIngredients.ToList();
            model.Options = db.Options.ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Name,Description,NormalSizedPrice,SmallSizedPrice,FamilySizedPrice,imageFile,SelectedIngredients,SelectedNoIngredients,SelectedOptions,FoodCategoryId,FootCategoryName")] FoodModel model)
        {
            Food food = new Food();
            food.Ingredients = new List<Ingredient>();
            food.NoIngredients = new List<NoIngredient>();
            food.Options = new List<Option>();

            if (ModelState.IsValid && model.imageFile != null)
            {
                food.Name = model.Name;
                food.Description = model.Description;
                food.NormalSizedPrice = model.NormalSizedPrice;
                food.SmallSizedPrice = model.SmallSizedPrice;
                food.FamilySizedPrice = model.FamilySizedPrice;

                if(model.SmallSizedPrice == 0 && model.FamilySizedPrice == 0)
                    food.disabledPrices = true;

                if (model.SelectedIngredients == null)
                {
                    model.SelectedIngredients = new List<int>();
                }

                if (model.SelectedNoIngredients == null)
                {
                    model.SelectedNoIngredients = new List<int>();
                }

                if (model.SelectedOptions == null)
                {
                    model.SelectedOptions = new List<int>();
                }

                foreach (int id in model.SelectedIngredients)
                {
                    Ingredient i = db.Ingredients.Find(id);
                    if (i == null)
                        return HttpNotFound();
                    food.Ingredients.Add(i);
                }

                foreach (int id in model.SelectedNoIngredients)
                {
                    NoIngredient ni = db.NoIngredients.Find(id);
                    if (ni == null)
                        return HttpNotFound();
                    food.NoIngredients.Add(ni);
                }

                foreach (int id in model.SelectedOptions)
                {
                    Option o = db.Options.Find(id);
                    if(o == null)
                        return HttpNotFound();
                    food.Options.Add(o);
                }

                string fileName = Path.GetFileName(model.imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                model.imageFile.SaveAs(path);
                food.ImageUrl = Url.Content("~/UploadedImages/" + fileName);

                var FoodCategory = db.FoodCategories.Find(model.FoodCategoryId);
                if (FoodCategory == null)
                    return HttpNotFound();
    
                FoodCategory.Foods.Add(food);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            model.Ingredients = db.Ingredients.ToList();
            model.NoIngredients = db.NoIngredients.ToList();
            model.Options = db.Options.ToList();

            return View(model);
        }

        // GET: Pizzas/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);

            if (food == null)
            {
                return HttpNotFound();
            }
            FoodModel model = new FoodModel();
            model.FoodCategoryId = food.FoodCategory.Id;
            model.Id = food.Id;
            model.Name = food.Name;
            model.Description = food.Description;
            model.NormalSizedPrice = food.NormalSizedPrice;
            model.SmallSizedPrice = food.SmallSizedPrice;
            model.FamilySizedPrice = food.FamilySizedPrice;
            model.disabledPrices = food.disabledPrices;
            model.imageUrl = food.ImageUrl;
            model.Ingredients = food.Ingredients;
            model.NoIngredients = food.NoIngredients;
            model.Options = food.Options;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,NormalSizedPrice,SmallSizedPrice,FamilySizedPrice,imageFile")] FoodModel model)
        {
            Food food = db.Foods.Find(model.Id);

            if (ModelState.IsValid)
            {
                if (model.imageFile != null)
                {
                    string fileName = Path.GetFileName(model.imageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedImages"), fileName);

                    model.imageFile.SaveAs(path);
                    food.ImageUrl = Url.Content("~/UploadedImages/" + fileName);
                }

                food.Name = model.Name;
                food.Description = model.Description;
                food.NormalSizedPrice = model.NormalSizedPrice; 
                food.SmallSizedPrice = model.SmallSizedPrice;
                food.FamilySizedPrice = model.FamilySizedPrice;
                food.Ingredients = model.Ingredients;
                food.NoIngredients = model.NoIngredients;
                food.Options = model.Options;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Pizzas/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Food food = db.Foods.Find(id);

            if (food == null)
                return HttpNotFound();

            db.Foods.Remove(food);
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
        [Authorize(Roles = "Administrator")]
        public ActionResult EditIngredients(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PizzaIngredientsModel model = new PizzaIngredientsModel();

            model.PizzaId = id.Value;
            model.Pizza = db.Foods.Find(id);

            if (model.Pizza == null)
                return HttpNotFound();

            model.AllIngredients = db.Ingredients.ToList();
            model.PizzaIngredients = model.Pizza.Ingredients.ToList();
            model.AllIngredients = model.AllIngredients.Where(i => !model.PizzaIngredients.Contains(i)).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditIngredients(PizzaIngredientsModel model)
        {
            if (ModelState.IsValid)
            {
                var Pizza = db.Foods.Find(model.PizzaId);

                if(Pizza  == null) return HttpNotFound();

                var Ingredient = db.Ingredients.Find(model.SelectedIngredientId);

                if (Ingredient == null) return HttpNotFound();

                Pizza.Ingredients.Add(Ingredient);
                db.SaveChanges();
                return RedirectToAction("EditIngredients");

            }
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteIngredient(int? pizzaId, int? ingredientId)
        {
            if(pizzaId == null || ingredientId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Food pizza = db.Foods.Find(pizzaId);

            if(pizza == null) return HttpNotFound();

            var ingredient = pizza.Ingredients.FirstOrDefault(i => i.Id == ingredientId);

            if (ingredient == null)
                return HttpNotFound();


            pizza.Ingredients.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult EditNoIngredients(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PizzaNoIngredientsModel model = new PizzaNoIngredientsModel();
            model.PizzaId = id.Value;

            model.Pizza = db.Foods.Find(id);

            if(model.Pizza == null) return HttpNotFound();

            model.AllNoIngredients = db.NoIngredients.ToList();
            model.PizzaNoIngredients = model.Pizza.NoIngredients.ToList();
            model.AllNoIngredients = model.AllNoIngredients.Where(i => !model.PizzaNoIngredients.Contains(i)).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditNoIngredients(PizzaNoIngredientsModel model)
        {
            if (ModelState.IsValid)
            {
                var Pizza = db.Foods.Find(model.PizzaId);

                if(Pizza  == null) return HttpNotFound();

                var NoIngredient = db.NoIngredients.Find(model.SelectedNoIngredientId);

                if(NoIngredient == null) return HttpNotFound();

                Pizza.NoIngredients.Add(NoIngredient);
                db.SaveChanges();

                return RedirectToAction("EditNoIngredients");
            }
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteNoIngredient(int? pizzaId, int? ingredientId)
        {
            if (pizzaId == null || ingredientId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Food pizza = db.Foods.Find(pizzaId);

            if(pizza == null)
                return HttpNotFound();

            var noIngredient = pizza.NoIngredients.FirstOrDefault(i => i.Id == ingredientId);

            if(noIngredient == null)
                return HttpNotFound();

            pizza.NoIngredients.Remove(noIngredient);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult EditOptions(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            PizzaOptionsModel model = new PizzaOptionsModel();
            model.PizzaId = id.Value;
            model.Pizza = db.Foods.Find(id);

            if(model.Pizza == null)
                return HttpNotFound();

            model.AllOptions = db.Options.ToList();
            model.PizzaOptions = model.Pizza.Options.ToList();
            model.AllOptions = model.AllOptions.Where(i => !model.PizzaOptions.Contains(i)).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditOptions(PizzaOptionsModel model)
        {
            if (ModelState.IsValid)
            {
                var Pizza = db.Foods.Find(model.PizzaId);

                if(Pizza == null)
                    return HttpNotFound();

                var Option = db.Options.Find(model.SelectedOptionId);

                if(Option == null)
                    return HttpNotFound();

                Pizza.Options.Add(Option);
                db.SaveChanges();
                return RedirectToAction("EditOptions");

            }
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteOption(int? pizzaId, int? ingredientId)
        {
            if (pizzaId == null || ingredientId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Food pizza = db.Foods.Find(pizzaId);

            if(pizza == null) return HttpNotFound();

            var option = pizza.Options.FirstOrDefault(i => i.Id == ingredientId);

            if(option == null) return HttpNotFound();

            pizza.Options.Remove(option);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Order(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Food Food = db.Foods.Find(id);

            if(Food == null)
                return HttpNotFound();

            FoodOrderModel model = new FoodOrderModel();

            model.Food = Food;
            model.FoodId = id.Value;
            model.SelectedSize = "Normal";
            model.Number = "1";

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Order(FoodOrderModel model)
        {
            
            if (ModelState.IsValid && model.SelectedSize != null && int.TryParse(model.Number, out int a))
            {
                int result = int.Parse(model.Number);
                Food Food = db.Foods.Find(model.FoodId);

                if(Food == null)
                    return HttpNotFound();

                FoodOrder FoodOrder = new FoodOrder();
                decimal TotalPrice = 0;

                if (model.SelectedIngredients == null)
                    model.SelectedIngredients = new List<int>();
                if (model.SelectedNoIngredients == null)
                    model.SelectedNoIngredients = new List<int>();
                if (model.SelectedOptions == null)
                    model.SelectedOptions = new List<int>();
                
                foreach (int ID in model.SelectedIngredients)
                {
                    Ingredient i = db.Ingredients.Find(ID);
                    if (i == null)
                        return HttpNotFound();

                    FoodOrder.SelectedIngredientsList.Add(i);
                    TotalPrice += i.Price;
                }

                foreach (int ID in model.SelectedNoIngredients)
                {
                    NoIngredient ni = db.NoIngredients.Find(ID);
                    if (ni == null)
                        return HttpNotFound();

                    FoodOrder.SelectedNoIngredientsList.Add(ni);
                    TotalPrice += ni.Price;
                }

                foreach (int ID in model.SelectedOptions)
                {
                    Option o = db.Options.Find(ID);
                    if (o == null) return HttpNotFound();

                    FoodOrder.SelectedOptionsList.Add(o);
                }

                FoodOrder.FoodId = model.FoodId;
                FoodOrder.SelectedSize = model.SelectedSize;

                string selectedSize = model.SelectedSize;
                FoodOrder.SelectedSize = selectedSize;
               
                if (selectedSize.Equals("Small"))
                    FoodOrder.FoodPrice = Food.SmallSizedPrice;
                else if (selectedSize.Equals("Normal"))
                    FoodOrder.FoodPrice = Food.NormalSizedPrice;
                else if (selectedSize.Equals("Family"))
                    FoodOrder.FoodPrice = Food.FamilySizedPrice;
                else FoodOrder.FoodPrice = -1;

                FoodOrder.Number = result;

                TotalPrice += FoodOrder.FoodPrice;
                FoodOrder.TotalPrice = TotalPrice * result;

                string userId = User.Identity.GetUserId();
                Cart cart = db.Carts.Where(c => c.UserId.Equals(userId) && !c.isOrdered).FirstOrDefault();

                bool inserted = false;

                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    inserted = true;
                }

                FoodOrder.UserId = userId;
                cart.FoodOrders.Add(FoodOrder);
                cart.TotalPrice += FoodOrder.TotalPrice;

                if (inserted)
                    db.Carts.Add(cart);
                db.SaveChanges();

                string location = Session["Location"] as string;

                if (location != null)
                    return RedirectToAction("Details", "FoodCategories", new { id = int.Parse(location) });
                return RedirectToAction("ClientIndex", "Carts");
            }
            Food Food2 = db.Foods.Find(model.FoodId);

            if (Food2 == null)
                return HttpNotFound();

            model.Food = Food2;

            return View(model);
        }
    }
}
