using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Web_App.Models
{
    public class PizzaNoIngredientsModel
    {
        public PizzaNoIngredientsModel()
        {
            PizzaId = -1;
            Pizza = null;
            AllNoIngredients = new List<NoIngredient>();
            PizzaNoIngredients = new List<NoIngredient>();
            SelectedNoIngredientId = -1;
        }
        public int PizzaId { get; set; }
        public Food Pizza { get; set; }
        public List<NoIngredient> AllNoIngredients { get; set; }
        public List<NoIngredient> PizzaNoIngredients { get; set; }
        public int SelectedNoIngredientId { get; set; }

    }
}