using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
            FoodCategories = new List<FoodCategory>();
            News = new List<New>();
        }
        public List<FoodCategory> FoodCategories { get; set; }
        public List<New> News { get; set; }
        public List<Day> Days { get; set; }
        public List<Location> Locations { get; set; }
        public List<Review> Reviews { get; set; }
        public string userId { get; set; }
        public ApplicationUser User { get; set; }
    }
}