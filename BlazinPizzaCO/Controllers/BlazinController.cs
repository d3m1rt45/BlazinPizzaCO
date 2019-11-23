using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using BlazinPizzaCO.ViewModels;
using Microsoft.AspNet.Identity;
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
            
                if (User.Identity.IsAuthenticated && orderID.HasValue)
                {
                    var memberID = User.Identity.GetUserId();
                    
                    order = await db.Orders.FindAsync(orderID);

                    if(order.MemberID != memberID)
                        throw new UnauthorizedAccessException("This is not your order.");
                }
                else if (User.Identity.IsAuthenticated)
                {
                    var memberID = User.Identity.GetUserId();
                    order = new Order { MemberID = memberID };
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();

                }
                else if (orderID.HasValue )
                {
                    var userIP = Request.UserHostAddress;
                    order = await db.Orders.FindAsync(orderID);

                    if (order.MemberID != userIP)
                        throw new UnauthorizedAccessException("This is not your order.");
                }
                else
                {
                    var userIP = Request.UserHostAddress;
                    order = new Order { MemberID = userIP };
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
                order.Pizzas.Add(pizza);
                await db.SaveChangesAsync();
            }

            var toppingVM = new ToppingViewModel { Pizza = pizza, Toppings = db.Toppings.ToList() };

            return await Task.Run(() => View(toppingVM));
        }

        public async Task<ActionResult> AddTopping(int pizzaID, string toppingName)
        {
            var pizza = await db.Pizzas.FindAsync(pizzaID);
            var top = await db.Toppings.FindAsync(toppingName);

            if (pizza.Toppings.Contains(top) == true)
            {
                pizza.Toppings.Remove(top);
            }
            else
            {
                pizza.Toppings.Add(top);
            }
            await db.SaveChangesAsync();

            return await Task.Run(() => RedirectToAction("PizzaTopping", new { orderID = pizza.Order.ID, size = pizza.Inches, pizzaID }));
        }

        public async Task<ActionResult> PizzaDone(int pizzaID)
        {
            var pizza = await db.Pizzas.FindAsync(pizzaID);

            pizza.Done = true;
            await db.SaveChangesAsync();

            return await Task.Run(() => RedirectToAction("Order", new { orderID = pizza.Order.ID }));
        }

        public async Task<ActionResult> RemovePizza(int orderID, int pizzaID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var pizza = await db.Pizzas.FindAsync(pizzaID);

            await Task.Run(() => order.Pizzas.Remove(pizza) );
            await db.SaveChangesAsync();

            return await Task.Run(() => RedirectToAction("ReviewOrder", new { orderID = order.ID }));
        }

        public async Task<ActionResult> ChooseSide(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var addSideVM = new SideViewModel() { Order = order };

            return await Task.Run(() => View(addSideVM));
        }

        public async Task<ActionResult> AddSide(int orderID, int sideID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var side = await db.Sides.FindAsync(sideID);

            await Task.Run(() => order.AddSide(side));

            return await Task.Run(() => RedirectToAction("ChooseSide", new { orderID }));
        }

        public async Task<ActionResult> RemoveSide(int sidesPerOrderID)
        {
            var sidesPerOrder = await db.SidePerOrder.FindAsync(sidesPerOrderID);
            var order = await db.Orders.FindAsync(sidesPerOrder.Order.ID);

            if (sidesPerOrder.Amount > 1)
            {
                sidesPerOrder.Amount -= 1;
                db.SaveChanges();
            }
            else
            {
                order.SidesPerOrder.Remove(sidesPerOrder);
                db.SaveChanges();
            }

            return await Task.Run(() => RedirectToAction("ReviewOrder", new { orderID = order.ID }));
        }

        public async Task<ActionResult> ChooseDrink(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var drinkVM = new DrinkViewModel(order);
            return await Task.Run(() => View(drinkVM));
        }

        public async Task<ActionResult> AddDrink(int orderID, int drinkID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var drink = await db.Drinks.FindAsync(drinkID);

            await Task.Run(() => order.AddDrink(drink));

            return await Task.Run(() => RedirectToAction("ChooseDrink", new { orderID }));
        }

        public async Task<ActionResult> ReviewOrder(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);
            await Task.Run(() => order.Refine());

            return await Task.Run(() => View(order));
        }
    }
}