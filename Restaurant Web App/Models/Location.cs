using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Time { get; set; }
    }
}