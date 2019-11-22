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
            Drinks = new List<Drink>();
            DrinksPerOrder = new List<DrinkPerOrder>();
            SidesPerOrder = new List<SidePerOrder>();
        }

        // Properties
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MemberID { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }

        public List<Drink> Drinks { get; set; }

        // Relationship Field(s)
        public virtual ICollection<Pizza> Pizzas { get; set; }
        public virtual ICollection<Side> Sides { get; set; }
        public virtual ICollection<DrinkPerOrder> DrinksPerOrder { get; set; }
        public virtual ICollection<SidePerOrder> SidesPerOrder { get; set; }

        //Methods
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
                    this.Sides.Add(side);
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
                    this.Drinks.Add(drink);
                    this.DrinksPerOrder.Add(new DrinkPerOrder(this.ID, drink.ID));
                    db.SaveChanges();
                }

            }
        }

        public decimal GetTotal()
        {
            var total = 0m;

            foreach (var p in this.Pizzas)
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

        public void Refine()
        {
            var discard = Pizzas.Where(p => !p.Done).ToList();
            discard.ForEach(p => Pizzas.Remove(p));
        }
    }
}