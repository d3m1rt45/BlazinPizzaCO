namespace BlazinPizzaCO.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlazinPizzaCO.DAL.BlazinContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlazinPizzaCO.DAL.BlazinContext context)
        {
            //POPULATE TOPPINGS...
            var toppings = new List<string>()
            {
                "Extra Cheese", "Black Olive", "Mushroom", "Pepperoni", "Olive Oil", "Onion","Sausage", "Green Pepper",
                    "Bacon", "Pineapple", "Spinach","Garlic", "Crushed Red Pepper", "Tomato", "Basil", "Ham"
            };

            foreach (var tp in toppings)
                context.Toppings.Add(new Topping { Name = tp });

            context.SaveChanges();



            //POPULATE SIDES...
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



            //POPULATE DRINKS...
            context.Drinks.Add(new Drink { Name = "Coca-Cola", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Dr. Pepper", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Fanta", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Sprite", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Mountain Dew", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Vimto", Price = 0.99m });
            context.Drinks.Add(new Drink { Name = "Ice-Tea Lemon", Price = 1.20m });
            context.Drinks.Add(new Drink { Name = "Ice-Tea Peach", Price = 1.20m });
            context.Drinks.Add(new Drink { Name = "Ice-Tea Mango", Price = 1.20m });

            context.SaveChanges();
        }
    }
}
