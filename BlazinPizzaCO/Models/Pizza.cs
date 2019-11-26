using BlazinPizzaCO.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Pizza
    {
        public Pizza()
        {
            Toppings = new List<Topping>();
            Done = false;
        }

        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Size Inches { get; set; }

        public bool Done { get; set; }

        // Relationship Field(s)
        public virtual Order Order { get; set; }
        public virtual ICollection<Topping> Toppings { get; set; }

        // Methods:
        public decimal GetPrice()
        {
            decimal price;

            switch(this.Inches)
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

            foreach(var top in Toppings)
            {
                price += 0.35m;
            }
            
            return price;
        }
    }

    // Enum types for this class
    public enum Size { Small, Medium, Large }
    
}