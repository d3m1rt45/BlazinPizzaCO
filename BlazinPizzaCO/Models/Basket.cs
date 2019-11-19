using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Basket
    {
        //Constructor(s)
        public Basket()
        {
            Pizzas = new List<Pizza>();
            Drinks = new List<Drink>();
            Sides = new List<Side>();
        }

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