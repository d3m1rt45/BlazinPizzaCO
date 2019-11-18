using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Drink
    {
        // Properties
        public int ID { get; set; }
        public SoftDrink Name { get; set; }
        public bool Large { get; set; }
        public decimal Price { get; set; }

        // Relationship Field(s)
        public virtual Basket Basket { get; set; }

        // Returns the price for the Drink object.
        public decimal GetPrice()
        {
            if (this.Large)
                this.Price += 1.5m;

            return Price;
        }
    }

    // Enum types for this class
    public enum SoftDrink { Coke, DrPepper, Fanta, Sprite, MountainDew, Vimto, RibenaBlackCurrant, RibenaVeryBerry }
}