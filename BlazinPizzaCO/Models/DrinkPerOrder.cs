using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class DrinkPerOrder
    {
        public DrinkPerOrder() { }

        public DrinkPerOrder(int orderID, int drinkID)
        {
            using (var db = new BlazinContext())
            {   
                //Get the related order and drink
                var order = db.Orders.Find(orderID);
                var drink = db.Drinks.Find(drinkID);


                if (order == null) //If there is no such order:
                {
                    throw new KeyNotFoundException("Order Not Found");
                }
                else if (drink == null) //If there is no such drink related to the said order:
                {
                    throw new KeyNotFoundException("Drink Not Found");
                }
                else
                {
                    this.Order = order;
                    this.Drink = drink;
                    this.Amount = 1;

                    db.DrinkPerOrder.Add(this);
                    db.SaveChanges();
                }
            }
        }

        public int ID { get; set; }
        public int Amount { get; set; }


        public virtual Order Order { get; set; }
        public virtual Drink Drink { get; set; }


        public void RaiseAmount()
        {
            using (var db = new BlazinContext())
            {
                var dpo = db.DrinkPerOrder.Single(d => d.ID == this.ID);

                int current = this.Amount;
                current++;
                dpo.Amount = current;

                db.SaveChanges();
            }
            
        }
    }
}