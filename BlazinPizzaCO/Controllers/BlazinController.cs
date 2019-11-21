using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using BlazinPizzaCO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlazinPizzaCO.Controllers
{

    public class BlazinController : Controller
    {
        public BlazinContext db = new BlazinContext();
        
        public async Task<ActionResult> Home()
        {
            return await Task.Run(() => View());
        }

        public async Task<ActionResult> Order(int? orderID)
        {
            Order order;

            if (orderID.HasValue)
            {
                order = await db.Orders.FindAsync(orderID);
            }
            else
            {
                order = new Order();
                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }

            return await Task.Run(() => View(order));
        }

        public async Task<ActionResult> PizzaSize(int orderID, int? pizzaID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (pizzaID.HasValue)
            {
                var pizza = db.Pizzas.Single(p => p.ID == pizzaID);
                order.Pizzas.Remove(pizza);
            }

            return await Task.Run(() => View(order));
        }

        public async Task<ActionResult> PizzaTopping(int orderID, Size size, int? pizzaID)
        {
            Pizza pizza;
            var order = await db.Orders.FindAsync(orderID);

            if (pizzaID.HasValue)
            {
                pizza = order.Pizzas.Single(p => p.ID == pizzaID);
            }
            else
            {
                pizza = new Pizza { Inches = size };
                order.Add(pizza);
                await db.SaveChangesAsync();
            }

            return await Task.Run(() => View(pizza));
        }

        public async Task<ActionResult> AddTopping(int pizzaID, string topping)
        {
            var pizza = await db.Pizzas.FindAsync(pizzaID);
            var top = await db.Toppings.FindAsync(topping);

            if (pizza.SelectedToppings.Contains(top) == true)
            {
                pizza.SelectedToppings.Remove(top);
            }
            else
            {
                pizza.SelectedToppings.Add(top);
                db.Pizzas.Append(pizza);
                await db.SaveChangesAsync();
            }

            return await Task.Run(() => RedirectToAction("PizzaTopping", new { orderID = pizza.Order.ID, size = pizza.Inches, pizzaID }));
        }
    }
}