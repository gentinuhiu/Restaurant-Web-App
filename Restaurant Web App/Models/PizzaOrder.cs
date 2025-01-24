using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class PizzaOrder
    {
        public PizzaOrder()
        {

        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Food Pizza { get; set; }
        public int PizzaId { get; set; }
        public string SelectedSize { get; set; }
        public decimal PizzaPrice { get; set; }
        public List<int> SelectedIngredients { get; set; }
        public List<int> SelectedNoIngredients { get; set; }
        public List<int> SelectedOptions { get; set; }
        public virtual List<Ingredient> SelectedIngredientsStr { get; set; }
        public virtual List<NoIngredient> SelectedNoIngredientsStr { get; set; }
        public virtual List<Option> SelectedOptionsStr { get; set; }
        public virtual List<Cart> Carts { get; set; }
        
    }
}