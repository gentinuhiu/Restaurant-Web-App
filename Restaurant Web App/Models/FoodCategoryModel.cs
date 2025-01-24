using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodCategoryModel
    {
        public FoodCategoryModel()
        {
            Name = "";
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public HttpPostedFileBase imageFile { get; set; }

    }
}