using Restaurant_Web_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Restaurant_Web_App.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            HomeModel HomeModel = new HomeModel();
            HomeModel.FoodCategories = db.FoodCategories.ToList();
            HomeModel.News = db.News.OrderByDescending(c => c.TimePosted).ToList();

            if(db.Days.ToList().Count != 7)
            {
                for(int i = 0; i < 7; i++)
                {
                    db.Days.Add(new Day("00.00", "00.00", "00.00", "00.00"));
                }
                db.SaveChanges();
            }

            HomeModel.Days = db.Days.ToList();
            HomeModel.Locations = db.Locations.ToList();
            HomeModel.Reviews = db.Reviews.OrderByDescending(r => r.DatePosted).ToList();

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                ApplicationUser user = db.Users.Find(userId);

                if(user != null)
                {
                    HomeModel.userId = userId;
                    HomeModel.User = user;
                }
            }

            foreach(Review R in HomeModel.Reviews)
            {
                ApplicationUser User = db.Users.Find(R.UserId);
                if (User != null)
                    R.User = User;
            }

            return View(HomeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}