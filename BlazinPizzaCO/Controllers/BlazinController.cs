using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlazinPizzaCO.Controllers
{

    public class BlazinController : Controller
    {
        public BlazinContext db = new BlazinContext();
        
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Order(int? orderID)
        {
            Order order;

            if (orderID.HasValue)
            {
                order = db.Orders.Single(o => o.ID == orderID);
            }
            else
            {
                order = new Order();
                db.Orders.Add(order);
                db.SaveChanges();
            }
            
            return View(order);
        }

        public ActionResult PizzaSize(int orderID)
        {
            var order = db.Orders.Single(o => o.ID == orderID);
            return View(order);
        }
    }
}