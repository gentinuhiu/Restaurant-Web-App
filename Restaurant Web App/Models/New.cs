using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurant_Web_App.Models
{
    public class New
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimePosted { get; set; }
    }
}