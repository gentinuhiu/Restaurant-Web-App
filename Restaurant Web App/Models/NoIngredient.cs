using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class NoIngredient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<Food> Pizzas { get; set; }
        public virtual List<PizzaOrder> PizzaOrders { get; set; }
        public virtual List<FoodOrder> FoodOrders { get; set; }


    }
}