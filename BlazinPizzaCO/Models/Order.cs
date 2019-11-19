using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Order
    {
        //Constructor(s)
        public Order()
        {
            Pizzas = new List<Pizza>();
            Drinks = new List<Drink>();
            Sides = new List<Side>();
        }

        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int MemberID { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }

        // Relationship Field(s)
        public virtual List<Pizza> Pizzas { get; set; }
        public virtual List<Drink> Drinks { get; set; }
        public virtual List<Side> Sides { get; set; }


        // Adds a pizza to the basket
        public void Add(Pizza pizza)
        {
            Pizzas.Add(pizza);
        }

        // Adds a side to the basket
        public void Add(Side side)
        {
            Sides.Add(side);
        }

        // Adds a drink to the basket
        public void Add(Drink drink)
        {
            Drinks.Add(drink);
        }
    }
}