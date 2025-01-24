using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal NormalSizedPrice { get; set; }
        [Required]
        public decimal SmallSizedPrice { get; set; }
        [Required]
        public decimal FamilySizedPrice { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public bool disabledPrices { get; set; }
        public string SelectedSize { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        public virtual List<NoIngredient> NoIngredients { get; set; }

        public virtual List<Option> Options { get; set; }
        public virtual List<PizzaOrder> PizzaOrders { get; set; }
        public virtual FoodCategory FoodCategory { get; set; }
    }
}