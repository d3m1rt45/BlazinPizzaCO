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
                "Extra Cheese", "Black Olive", "Mushroom", "Pepperoni", "Olive Oil", "Onion","Sausage", "Green Pepper",
                    "Bacon", "Pineapple", "Spinach","Garlic", "Crushed Red Pepper", "Tomato", "Basil", "Ham"
            };

            foreach (var tp in toppings)
                context.Toppings.Add(new Topping { Name = tp });

            context.SaveChanges();


            context.Sides.Add(new Side { Name = "Chicken Wings", Price = 3.19m });
            context.Sides.Add(new Side { Name = "Garlic Bread", Price = 3.99m });
            context.Sides.Add(new Side { Name = "Potato Chips", Price = 3.29m });
            context.Sides.Add(new Side { Name = "Onion Rings", Price = 2.49m });
            context.Sides.Add(new Side { Name = "Mozzarella Bites", Price = 2.49m });
            context.Sides.Add(new Side { Name = "Prosciutto Bites", Price = 3.19m });
            context.Sides.Add(new Side { Name = "Brownie", Price = 2.99m });
            context.Sides.Add(new Side { Name = "Apple Tart", Price = 2.99m });
            context.Sides.Add(new Side { Name = "Chocolate Chip Cookie", Price = 0.99m });

            context.SaveChanges();
        }
    }
}