using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.DAL
{
    public class BlazinContextInitializer : DropCreateDatabaseAlways<BlazinContext>
    {
        protected override void Seed(BlazinContext context)
        {
            var toppings = new List<string>()
            {
                "Extra Cheese", "Black Olive", "Mushroom", "Pepperoni", "Olive Oil", "Onion","Sausage", "GreenPepper",
                    "Bacon", "Pineapple", "Spinach","Garlic", "Crushed Red Pepper", "Tomato", "Basil", "Ham"
            };

            foreach (var tp in toppings)
                context.Toppings.Add(new Topping { Name = tp });

            context.SaveChanges();
        }
    }
}