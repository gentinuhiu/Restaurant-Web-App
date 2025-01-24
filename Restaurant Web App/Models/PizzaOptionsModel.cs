using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Web_App.Models
{
    public class PizzaOptionsModel
    {
        public PizzaOptionsModel()
        {
            PizzaId = -1;
            Pizza = null;
            AllOptions = new List<Option>();
            PizzaOptions = new List<Option>();
            SelectedOptionId = -1;
        }
        public int PizzaId { get; set; }
        public Food Pizza { get; set; }
        public List<Option> AllOptions { get; set; }
        public List<Option> PizzaOptions { get; set; }
        public int SelectedOptionId { get; set; }

    }
}