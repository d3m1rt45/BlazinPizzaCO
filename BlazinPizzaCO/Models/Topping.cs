using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Topping
    {
        public Topping() 
        {
            Pizzas = new List<Pizza>();
        }

        [Key]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}