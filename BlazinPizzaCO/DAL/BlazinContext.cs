using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.DAL
{
    public class BlazinContext : DbContext
    {
        public BlazinContext() : base("name=BlazinContext") 
        {
            Database.SetInitializer<BlazinContext>(new DropCreateDatabaseAlways<BlazinContext>());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Side> Sides { get; set; }
        public DbSet<Topping> Toppings { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //public readonly List<string> AvailableToppings = new List<string>()
        //{
        //    "Extra Cheese", "Black Olive", "Mushroom", "Pepperoni", "Olive Oil", "Onion","Sausage", "GreenPepper",
        //        "Bacon", "Pineapple", "Spinach","Garlic", "Crushed Red Pepper", "Tomato", "Basil", "Ham"
        //};
    }

    public class BlazinContextInitializer : DropCreateDatabaseAlways<BlazinContext>
    {
        protected override void Seed(BlazinContext context)
        {
            
    }
    }
}