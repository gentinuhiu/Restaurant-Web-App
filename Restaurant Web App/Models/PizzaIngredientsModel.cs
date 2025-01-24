using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Web_App.Models
{
    public class PizzaIngredientsModel
    {
        public PizzaIngredientsModel()
        {
            PizzaId = -1;
            Pizza = null;
            AllIngredients = new List<Ingredient>();
            PizzaIngredients = new List<Ingredient>();
            SelectedIngredientId = -1;
        }
        public int PizzaId { get; set; }
        public Food Pizza { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
        public List<Ingredient> PizzaIngredients { get; set; }
        public int SelectedIngredientId { get; set; }

    }
}