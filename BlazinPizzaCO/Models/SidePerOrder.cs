using BlazinPizzaCO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Models
{
    public class SidePerOrder
    {
        public SidePerOrder() { }

        public SidePerOrder(int orderID, int sideID)
        {
            using (var db = new BlazinContext())
            {
                //Get the related order and drink
                var order = db.Orders.Find(orderID);
                var side = db.Sides.Find(sideID);


                if (order == null) //If there is no such order:
                {
                    throw new KeyNotFoundException("Order Not Found");
                }
                else if (side == null) //If there is no such drink related to the said order:
                {
                    throw new KeyNotFoundException("Drink Not Found");
                }
                else
                {
                    this.Order = order;
                    this.Side = side;
                    this.Amount = 1;

                    db.SidesPerOrder.Add(this);
                    db.SaveChanges();
                }
            }
        }

        public int ID { get; set; }
        public int Amount { get; set; }


        public virtual Order Order { get; set; }
        public virtual Side Side { get; set; }


        public void RaiseAmount()
        {
            using (var db = new BlazinContext())
            {
                var dpo = db.SidesPerOrder.Single(d => d.ID == this.ID);

                int current = this.Amount;
                current++;
                dpo.Amount = current;

                db.SaveChanges();
            }

        }
    }
}