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
            DrinksPerOrder = new List<DrinkPerOrder>();
            SidesPerOrder = new List<SidePerOrder>();
        }


        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public decimal Total { get; set; }
        public bool Submitted { get; set; }
        public decimal Points { get; set; }

        
        // Relationship Field(s)
        public virtual Member Member { get; set; }
        public virtual PaymentDetails PaymentDetails { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<DrinkPerOrder> DrinksPerOrder { get; set; }
        public virtual ICollection<SidePerOrder> SidesPerOrder { get; set; }


        //Methods

        //If an order with the given orderID found in the database, return it. Otherwise, create a new one and return that.
        public static Order FindOrCreate(BlazinContext db, int? orderID)
        {
            if(orderID.HasValue)
            {
                var order = db.Orders.Find(orderID.Value);

                if(order == null)
                    throw new KeyNotFoundException("Invalid Order ID.");
                else
                    return order;
            }
            else
            {
                var order = new Order();
                db.Orders.Add(order);
                db.SaveChanges();
                return order;
            }
            
        }

        public void AddSide(Side side)
        {
            using (var db = new BlazinContext())
            {
                var sidesPerOrder = this.SidesPerOrder.SingleOrDefault(spo => spo.Side.ID == side.ID);

                if (sidesPerOrder != null)
                {
                    sidesPerOrder.RaiseAmount();
                }
                else
                {
                    this.SidesPerOrder.Add(new SidePerOrder(this.ID, side.ID));
                    db.SaveChanges();
                }

            }
        }

        public void AddDrink(Drink drink)
        {
            using (var db = new BlazinContext())
            {
                var drinkPerOrder = this.DrinksPerOrder.SingleOrDefault(dpo => dpo.Drink.ID == drink.ID);

                if (drinkPerOrder != null)
                {
                    drinkPerOrder.RaiseAmount();
                }
                else
                {
                    this.DrinksPerOrder.Add(new DrinkPerOrder(this.ID, drink.ID));
                    db.SaveChanges();
                }

            }
        }

        public decimal GetTotal()
        {
            var total = 0m;

            foreach (var p in this.Pizzas.Where(p => p.Done))
                total += p.GetPrice();

            total += this.GetSideTotal();
            total += this.GetDrinkTotal();

            return total;
        }

        public decimal GetSideTotal()
        {
            decimal total = 0m;
            foreach (var spo in SidesPerOrder)
            {
                total += spo.Side.Price * spo.Amount;
            }
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

        public void Submit()
        {
            var discard = Pizzas.Where(p => !p.Done).ToList();
            discard.ForEach(p => Pizzas.Remove(p));
            this.Submitted = true;
            this.Points = this.GetTotal() * 0.25m;
        }
    }
}