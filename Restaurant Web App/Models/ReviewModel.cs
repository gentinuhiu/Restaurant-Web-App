using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class ReviewModel
    {
        public ReviewModel()
        {
            Reviews = new List<Review>();
        }
        public int Id { get; set; }
        public List<Review> Reviews { get; set; }
        public Review MyReview { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}