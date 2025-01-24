using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class Review
    {
        public Review()
        {
            DatePosted = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5 stars.")]
        public int Rating { get; set; }
    }
}