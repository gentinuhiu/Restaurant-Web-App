using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class Cart
    {
        public Cart()
        {
            PizzaOrders = new List<PizzaOrder>();
            FoodOrders = new List<FoodOrder>();
            isOrdered = false;
            isPrepared = false;
            AdminDeleted = false;
            ClientDeleted = false;
            OrderedTime = DateTime.Now;
            PreparedTime = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual List<PizzaOrder> PizzaOrders { get; set; }
        public virtual List<FoodOrder> FoodOrders { get; set; }
        public decimal TotalPrice { get; set; }
        public bool isOrdered { get; set; }
        public bool isPrepared { get; set; }
        public bool AdminDeleted { get; set; }
        public bool ClientDeleted { get; set; }
        public DateTime OrderedTime { get; set; }
        public DateTime PreparedTime { get; set; }

    }
}