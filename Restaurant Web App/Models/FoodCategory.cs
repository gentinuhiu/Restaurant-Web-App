﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public virtual List<Food> Foods { get; set; }
    }
}