using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.ViewModels
{
    public class DrinkViewModel
    {
        public DrinkViewModel(Order order)
        {
            this.Order = order;

            using (var db = new BlazinContext())
            {
                this.Drinks = db.Drinks.ToList();
            }
        }

        public Order Order { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}