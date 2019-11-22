using BlazinPizzaCO.DAL;
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
                var order = db.Orders.Find(orderID);
                var drink = db.Drinks.Find(drinkID);

                if (order == null)
                {
                    throw new KeyNotFoundException("Order Not Found");
                }
                else if (drink == null)
                {
                    throw new KeyNotFoundException("Drink Not Found");
                }
                else
                {
                    this.Order = order;
                    this.OrderID = orderID;

                    this.Drink = drink;
                    this.DrinkID = drinkID;

                    db.SaveChanges();
                }
            }
        }

        public int ID { get; set; }
        public int OrderID { get; set; }
        public int DrinkID { get; set; }
        public int Amount { get; set; }


        public virtual Order Order { get; set; }
        public virtual Drink Drink { get; set; }


    }
}