using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodCategoryModel2
    {
        public FoodCategoryModel2()
        {
            Foods = new List<Food>();
            Days = new List<Day>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public List<Food> Foods { get; set; }
        public List<Day> Days { get; set; }
    }
}