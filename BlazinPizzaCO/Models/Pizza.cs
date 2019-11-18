using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Pizza
    {
        // Properties
        public int ID { get; set; }
        public Size Size { get; set; }
        public List<Topping> Toppings { get; set; }

        // Relationship Field(s)
        public virtual Basket Basket { get; set; }

        // Returns Price
        public decimal GetPrice()
        {
            decimal price;

            switch(this.Size)
            {
                case Size.Small:
                    price = 8.99m;
                    break;
                case Size.Medium:
                    price = 10.99m;
                    break;
                case Size.Large:
                    price = 12.99m;
                    break;
                default:
                    throw new Exception("Something is not right with the size...");
            }

            var paidToppings = Toppings.Count - 3;
            if (paidToppings > 0)
            {
                price += paidToppings * 0.35m;
            }

            return price;
        }
    }

    // Enum types for this class
    public enum Size { Small, Medium, Large }
    public enum Topping { ExtraCheese, BlackOlive, Mushroom, Pepperoni, OliveOil, Onion, Sausage, GreenPepper, 
                          Bacon, Pineapple, Spinach, Garlic, CrushedRedPepper, Tomato, Basil, Ham }
}