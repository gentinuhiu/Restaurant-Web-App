using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Restaurant_Web_App.Models;

namespace Restaurant_Web_App.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        [AllowAnonymous]
        public ActionResult Index()
        {
            ReviewModel model = new ReviewModel();
            model.Reviews = db.Reviews.OrderByDescending(r => r.DatePosted).ToList();

            foreach(Review r in model.Reviews)
            {
                ApplicationUser User = db.Users.Find(r.UserId);

                if (User != null)
                    r.User = User;
            }

            if (User.Identity.IsAuthenticated)
            {
                model.MyReview = model.Reviews.Where(r => r.UserId.Equals(User.Identity.GetUserId())).FirstOrDefault();
                model.UserId = User.Identity.GetUserId();
            }
            return View(model);
        }

        // GET: Reviews/Details/5
        /*
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }*/

        // GET: Reviews/Create
        /*[Authorize]
        public ActionResult Create()
        {
            return View();
        }
        */
        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comment,Rating")] Review review)
        {
            if (review.Comment == null || review.Rating == null)
                return RedirectToAction("Index");

            review.UserId = User.Identity.GetUserId();
            review.DatePosted = DateTime.Now;
            Review databaseReview = db.Reviews.Where(c => c.UserId.Equals(review.UserId)).FirstOrDefault();
            if (ModelState.IsValid && databaseReview == null)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        // GET: Reviews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            if (!User.Identity.GetUserId().Equals(review.UserId))
                return RedirectToAction("Index", "Home");

            ReviewModel model = new ReviewModel();
            model.UserId = review.UserId;
            model.Id = review.Id;
            model.Reviews = db.Reviews.OrderByDescending(r => r.DatePosted).ToList();


            foreach (Review r in model.Reviews)
            {
                ApplicationUser User = db.Users.Find(r.UserId);

                if (User != null)
                    r.User = User;
            }

            model.Comment = review.Comment;
            model.Rating = review.Rating;

            return View(model);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(ReviewModel reviewModel)
        {

            if (reviewModel.Comment == null || reviewModel.Rating == null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                Review Review = db.Reviews.Find(reviewModel.Id);

                if(Review == null)
                {
                    return HttpNotFound();
                }
                Review.UserId = reviewModel.UserId;
                Review.Comment = reviewModel.Comment;
                Review.Rating = reviewModel.Rating;
                reviewModel.MyReview = Review;

                if(!User.Identity.GetUserId().Equals(reviewModel.UserId))
                {
                    return RedirectToAction("Index", "Home");
                }    

                reviewModel.MyReview.DatePosted = DateTime.Now;
                db.Entry(reviewModel.MyReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reviewModel);
        }

        // GET: Reviews/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            if (!User.Identity.GetUserId().Equals(review.UserId))
                return RedirectToAction("Index", "Home");
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);

            if (!review.UserId.Equals(User.Identity.GetUserId()))
            {
                return RedirectToAction("Index", "Home");
            }

            db.Reviews.Remove(review);
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
