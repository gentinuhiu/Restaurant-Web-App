using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class OrderIngredient
    {
        [Key]
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int OrderId { get; set; }
        public virtual List<FoodOrder> FoodOrders { get; set; }
    }
}