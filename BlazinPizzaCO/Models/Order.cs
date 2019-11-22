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
        public string MemberID { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }

        // Relationship Field(s)
        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<Side> Sides { get; set; }


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


        //Methods
        public decimal GetSideTotal()
        {
            decimal total = 0m;

            foreach (var s in Sides)
            {
                total += s.Price;
            }

            return total;
        }
    }
}