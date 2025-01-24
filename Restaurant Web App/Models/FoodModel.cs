using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class FoodModel
    {

        public int Id { get; set; }
        public List<FoodCategory> FoodCategories { get; set; }
        public int FoodCategoryId { get; set; }
        public string FoodCategoryName { get; set; }
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
        public bool disabledPrices { get; set; }
        public HttpPostedFileBase imageFile { get; set; }
        public string imageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<NoIngredient> NoIngredients { get; set; }
        public List<Option> Options { get; set; }
        public List<int> SelectedIngredients { get; set; }
        public List<int> SelectedNoIngredients { get; set; }
        public List<int> SelectedOptions { get; set; }
    }
}