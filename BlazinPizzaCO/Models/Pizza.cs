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
            SelectedToppings = new List<Topping>();
            AvailableToppings = new List<Topping>();
            using (var db = new BlazinContext())
            {
                AvailableToppings.AddRange(db.Toppings.ToList());
            }
        }

        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Size Inches { get; set; }
        public List<Topping> AvailableToppings { get; set; }

        // Relationship Field(s)
        public virtual Order Order { get; set; }
        public virtual List<Topping> SelectedToppings { get; set; }

        // Returns Price
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

            var paidToppings = SelectedToppings.Count - 3;
            if (paidToppings > 0)
            {
                price += paidToppings * 0.35m;
            }

            return price;
        }
    }
    // Enum types for this class
    public enum Size { Small, Medium, Large }
    
}