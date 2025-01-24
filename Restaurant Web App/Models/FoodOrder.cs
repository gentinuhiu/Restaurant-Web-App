using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodOrder
    {
        public FoodOrder()
        {
            SelectedIngredientsList = new List<Ingredient>();
            SelectedNoIngredientsList = new List<NoIngredient>();
            SelectedOptionsList = new List<Option>();
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Food Food { get; set; }
        public int FoodId { get; set; }
        public string SelectedSize { get; set; }
        public int Number { get; set; }
        public decimal FoodPrice { get; set; }
        public virtual List<Ingredient> SelectedIngredientsList { get; set; }
        public virtual List<NoIngredient> SelectedNoIngredientsList { get; set; }
        public virtual List<Option> SelectedOptionsList { get; set; }
        public decimal TotalPrice { get; set; }
    }
}