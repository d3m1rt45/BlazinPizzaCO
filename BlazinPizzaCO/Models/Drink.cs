using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Drink
    {
        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Large { get; set; }
        public decimal Price { get; set; }

        // Relationship Field(s)
        public virtual Order Order { get; set; }

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