using System.Net;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Stripe;
using Restaurant_Web_App.Models;
using System.Linq;

namespace YourNamespace.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly string _stripeSecretKey = System.Configuration.ConfigurationManager.AppSettings["StripeSecretKey"];
        private readonly string _stripePublishableKey = System.Configuration.ConfigurationManager.AppSettings["StripePublishableKey"];
        public PaymentController()
        {
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        public ActionResult Index()
        {
            ViewBag.PublishableKey = _stripePublishableKey;
            return View();
        }

        [HttpPost]
        public ActionResult CreateCharge(string stripeEmail, string stripeToken)
        {
            /*

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                Cart Cart = db.Carts.Where(c => c.UserId.Equals(userId) && !c.isOrdered).FirstOrDefault();

                if (Cart == null)
                    return HttpNotFound();

                Cart.isOrdered = true;
                Cart.OrderedTime = DateTime.Now;
                db.SaveChanges();

            }

            */

            if (stripeEmail == null || stripeToken == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            List<Cart> Carts = db.Carts.ToList();

            foreach(Cart cart in Carts)
            {
                ApplicationUser ApplicationUser = db.Users.Find(cart.UserId);

                if(ApplicationUser != null)
                {
                    cart.User = ApplicationUser;
                }
            }

            string userId = User.Identity.GetUserId();

            Cart Cart = Carts.Where(c => c.User != null && c.UserId.Equals(userId) && c.User.Email.Equals(stripeEmail) && !c.isOrdered).FirstOrDefault();

            if (Cart == null)
                return HttpNotFound();

            try
            {
                var customers = new CustomerService();
                var charges = new ChargeService();

                var customer = customers.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Source = stripeToken
                });

                var charge = charges.Create(new ChargeCreateOptions
                {
                    Amount = (int)(Cart.TotalPrice * 100), // Convert CHF to cents
                    Description = "Subito Pizzakurier - Order " + Cart.Id,
                    Currency = "chf",
                    Customer = customer.Id
                });

                Cart.isOrdered = true;
                Cart.OrderedTime = DateTime.Now;
                db.SaveChanges();

                //return RedirectToAction(Cart.User.Email);
                return View("ChargeResult", Cart);
            }
            catch (StripeException ex)
            {
                // Log the error or handle it as needed
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }
    }
}
