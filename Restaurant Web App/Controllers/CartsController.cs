using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string _stripeSecretKey = System.Configuration.ConfigurationManager.AppSettings["StripeSecretKey"];
        private readonly string _stripePublishableKey = System.Configuration.ConfigurationManager.AppSettings["StripePublishableKey"];

        // GET: Carts
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<Cart> carts = db.Carts.Where(c => c.isOrdered && !c.isPrepared && !c.AdminDeleted).OrderByDescending(c => c.OrderedTime).ToList();

            if (carts == null)
                carts = new List<Cart>();

            foreach (Cart c in carts)
            {
                ApplicationUser User = db.Users.Find(c.UserId);

                if (User == null)
                    return HttpNotFound();
                
                c.User = User;

                foreach (FoodOrder FO in c.FoodOrders)
                {
                    Food Food = db.Foods.Find(FO.FoodId);

                    if (Food == null)
                        return HttpNotFound();

                    FO.Food = Food;
                }
            }
           
            return View(carts);
        }

        [Authorize(Roles = "Administrator")]
        public JsonResult GetUpcomingOrders()
        {
            var upcomingOrders = db.Carts
                                  .Where(c => c.isOrdered && !c.isPrepared && !c.AdminDeleted)
                                  .OrderByDescending(c => c.OrderedTime)
                                  .Select(c => new
                                  {
                                      Id = c.Id,
                                      UserName = c.User.Name + " " + c.User.Surname,
                                      Email = c.User.Email,
                                      PhoneNumber = c.User.PhoneNumber,
                                      Address = c.User.Address + ", " + c.User.City,
                                      TotalPrice = c.TotalPrice,
                                      OrderedTime = c.OrderedTime,
                                      FoodOrders = c.FoodOrders.Select(fo => new
                                      {
                                          FoodName = fo.Food.Name,
                                          SelectedSize = fo.SelectedSize,
                                          Quantity = fo.Number,
                                          Ingredients = fo.SelectedIngredientsList.Select(i => i.Name),
                                          NoIngredients = fo.SelectedNoIngredientsList.Select(i => i.Name),
                                          Options = fo.SelectedOptionsList.Select(o => o.Name)
                                      })
                                  })
                                  .ToList();

            return Json(upcomingOrders, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult ClientIndex()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                Cart Cart = db.Carts.Where(C => C.UserId.Equals(userId) && !C.isOrdered && !C.ClientDeleted).FirstOrDefault();

                if(Cart == null)
                {
                    Cart = new Cart();
                    Cart.UserId = userId;
                    db.Carts.Add(Cart);
                    db.SaveChanges();
                }

                Cart.User = db.Users.Find(userId);

                if (Cart.User == null)
                    return HttpNotFound();

                foreach(FoodOrder FO in Cart.FoodOrders)
                {
                    Food Food = db.Foods.Find(FO.FoodId);

                    if (Food == null)
                        return HttpNotFound();

                    FO.Food = Food;
                }

                ViewBag.PublishableKey = _stripePublishableKey;
                return View(Cart);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult FinishedOrders()
        {
            List<Cart> Carts = db.Carts.Where(c => c.isOrdered && c.isPrepared && !c.AdminDeleted).OrderByDescending(c => c.PreparedTime).ToList();

            if (Carts == null)
                Carts = new List<Cart>();

            foreach(Cart Cart in Carts)
            {
                ApplicationUser User = db.Users.Find(Cart.UserId);

                if (User == null)
                    return HttpNotFound();
                
                Cart.User = User;
            }
            return View(Carts);
        }
        [Authorize]
        public ActionResult AllClientOrders()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                //List<Cart> Carts = db.Carts.Where(c => c.UserId.Equals(userId) && c.isOrdered && !c.ClientDeleted).OrderByDescending(c => c.PreparedTime).ToList();
                List<Cart> notCompleted = db.Carts.Where(c => c.UserId.Equals(userId) && c.isOrdered && !c.isPrepared && !c.ClientDeleted).OrderByDescending(c => c.OrderedTime).ToList();
                List<Cart> completed = db.Carts.Where(c => c.UserId.Equals(userId) && c.isOrdered && c.isPrepared && !c.ClientDeleted).OrderByDescending(c => c.PreparedTime).ToList();

                List<Cart> Carts = notCompleted;
                Carts.AddRange(completed);
                
                if (Carts == null)
                    Carts = new List<Cart>();

                return View(Carts);
             }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult RemoveOrder(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                Cart Cart = db.Carts.Where(c => c.UserId.Equals(userId) && !c.isOrdered).FirstOrDefault();

                if (Cart == null)
                    return HttpNotFound();

                FoodOrder FO = Cart.FoodOrders.Where(fo => fo.Id == id).FirstOrDefault();

                if(FO == null)
                    return HttpNotFound();

                Cart.FoodOrders.Remove(FO);
                Cart.TotalPrice -= FO.TotalPrice;
                db.SaveChanges();
            }
   
            return RedirectToAction("ClientIndex");
        }
        // GET: Carts/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            cart.User = db.Users.Find(cart.UserId);

            return View(cart);
        }

        // GET: Carts/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Carts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Search(int? Id)
        {
            if (Id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cart Cart = db.Carts.Find(Id);

            if (Cart == null || Cart.AdminDeleted)
                return HttpNotFound();

            List<Cart> Carts = new List<Cart>();
            Carts.Add(Cart);

            foreach(Cart c in Carts)
            {
                ApplicationUser User = db.Users.Find(c.UserId);

                if (User == null)
                    return HttpNotFound();

                c.User = User;
            }

            return View(Carts);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            if(User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Find(userId);

                if (user == null)
                    return HttpNotFound();

                cart.User = user;
            }
            return View(cart);
        }
        [Authorize]
        public ActionResult ClientDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }

            if(!cart.isPrepared)
                return RedirectToAction("AllClientOrders");

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                if (!cart.UserId.Equals(userId))
                    return RedirectToAction("Index", "Home");

                ApplicationUser user = db.Users.Find(userId);

                if (user == null)
                    return HttpNotFound();

                cart.User = user;
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if(id == null) 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cart cart = db.Carts.Find(id);

            if(cart == null)
                return HttpNotFound();

            if ((!cart.isPrepared) || (cart.ClientDeleted))
            {
                cart.FoodOrders.RemoveRange(0, cart.FoodOrders.Count);
                db.Carts.Remove(cart);
            }
            else
            {
                cart.AdminDeleted = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ClientDeleteConfirmed(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cart cart = db.Carts.Find(id);

            if (cart == null)
                return HttpNotFound();

            string userId = User.Identity.GetUserId();

            if (!cart.UserId.Equals(userId))
                return RedirectToAction("Index", "Home");

            if (cart.AdminDeleted)
            {
                cart.FoodOrders.RemoveRange(0, cart.FoodOrders.Count);
                db.Carts.Remove(cart);
            }
            else
            {
                cart.ClientDeleted = true;
            }
            db.SaveChanges();
            return RedirectToAction("AllClientOrders");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /*
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Order(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                Cart Cart = db.Carts.Where(c => c.UserId.Equals(userId) && !c.isOrdered).FirstOrDefault();

                if(Cart == null)
                    return HttpNotFound();

                Cart.isOrdered = true;
                Cart.OrderedTime = DateTime.Now;
                db.SaveChanges();

            }
            return RedirectToAction("Index", "Home");
        }
        */
        [Authorize(Roles = "Administrator")]
        public ActionResult FinishOrder(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Cart Cart = db.Carts.Find(id);

            if(Cart == null)
                return HttpNotFound();

            Cart.isPrepared = true;
            Cart.PreparedTime = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
