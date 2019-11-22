using BlazinPizzaCO.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class Order
    {
        //Constructor(s)
        public Order()
        {
            Pizzas = new List<Pizza>();
            Sides = new List<Side>();
            DrinksPerOrder = new List<DrinkPerOrder>();
        }

        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MemberID { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }

        // Relationship Field(s)
        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Side> Sides { get; set; }
        public virtual ICollection<DrinkPerOrder> DrinksPerOrder { get; set; }

        //Methods
        public void AddDrink(Drink drink)
        {
            using (var db = new BlazinContext())
            {
                var orderID = this.ID;
                var drinkID = drink.ID;
                var drinkPerOrderList = db.DrinkPerOrder.Where(dpo => dpo.DrinkID == drinkID && dpo.OrderID == orderID);

                if(drinkPerOrderList.Any())
                {
                    var drinkPerOrder = drinkPerOrderList.FirstOrDefault();
                    drinkPerOrder.Amount++;
                }
                else
                {
                    this.DrinksPerOrder.Add(new DrinkPerOrder(orderID, drinkID));
                }
            }
        }

        public decimal GetSideTotal()
        {
            decimal total = 0m;
            foreach (var s in Sides)
                total += s.Price;
            return total;
        }

        public decimal GetDrinkTotal()
        {
            decimal total = 0m;
            foreach (var dpo in DrinksPerOrder)
            {
                total += dpo.Drink.Price * dpo.Amount;
            }

            return total;
        }
    }
}