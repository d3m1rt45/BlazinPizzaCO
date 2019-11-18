using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Basket
    {
        // Properties
        public int ID { get; set; }
        public int MemberID { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }

        // Relationship Field(s)
        public List<Pizza> Pizzas { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Side> Sides { get; set; }


        // Adds a pizza to the basket
        public void Add(Pizza pizza)
        {
            if (Pizzas == null)
                Pizzas = new List<Pizza>();

            Pizzas.Add(pizza);
        }

        // Adds a side to the basket
        public void Add(Side side)
        {
            if (Sides == null)
                Sides = new List<Side>();

            Sides.Add(side);
        }

        // Adds a drink to the basket
        public void Add(Drink drink)
        {
            if (Drinks == null)
                Drinks = new List<Drink>();

            Drinks.Add(drink);
        }
    }
}