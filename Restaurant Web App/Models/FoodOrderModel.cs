using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodOrderModel
    {
        public FoodOrderModel()
        {
            SelectedIngredients = new List<int>();
            SelectedNoIngredients = new List<int>();
            SelectedOptions = new List<int>();
            SelectedIngredientsStr = new List<Ingredient>();
            SelectedNoIngredientsStr = new List<NoIngredient>();
            SelectedOptionsStr = new List<Option>();
        }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public string SelectedSize { get; set; }
        public string Number { get; set; }
        public List<int> SelectedIngredients { get; set; }
        public List<int> SelectedNoIngredients { get; set; }
        public List<int> SelectedOptions { get; set; }
        public List<Ingredient> SelectedIngredientsStr { get; set; }
        public List<NoIngredient> SelectedNoIngredientsStr { get; set; }
        public List<Option> SelectedOptionsStr { get; set; }
    }
}