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
            if(User.Identity.IsAuthenticated)
                return await Task.Run(() => RedirectToAction("Order"));
            else
                return await Task.Run(() => View());
        }

        public async Task<ActionResult> Order(int? orderID)
        {
            var order = Models.Order.FindOrCreate(db, orderID);

            if(User.Identity.IsAuthenticated)
            {
                var member = await db.Members.FindAsync(User.Identity.GetUserId());

                if(member == null)
                {
                    member = new Member(User.Identity.GetUserId());
                    db.Members.Add(member);
                    await db.SaveChangesAsync();
                }
            }

            return await Task.Run(() => View(order));
        }

        public async Task<ActionResult> PizzaSize(int orderID, int? pizzaID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

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

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

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

        public ActionResult AddTopping(int pizzaID, string toppingName)
        {
            var pizza = db.Pizzas.Find(pizzaID);
            var top = db.Toppings.Find(toppingName);

            if (pizza.Order.Submitted)
                return RedirectToAction("Order");

            if (pizza.Toppings.Contains(top) == true)
            {
                pizza.Toppings.Remove(top);
            }
            else
            {
                pizza.Toppings.Add(top);
            }
            db.SaveChanges();

            if(pizza.Free)
            {
                return RedirectToAction("FreePizza", new { pizzaID });
            }
            else
            {
                return RedirectToAction("PizzaTopping", new { orderID = pizza.Order.ID, size = pizza.Inches, pizzaID });
            }
        }

        public async Task<ActionResult> PizzaDone(int pizzaID)
        {
            var pizza = await db.Pizzas.FindAsync(pizzaID);

            if (pizza.Order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            pizza.Done = true;
            await db.SaveChangesAsync();

            return await Task.Run(() => RedirectToAction("Order", new { orderID = pizza.Order.ID }));
        }

        public async Task<ActionResult> RemovePizza(int orderID, int pizzaID)
        {
            var order = await db.Orders.FindAsync(orderID);
            var pizza = await db.Pizzas.FindAsync(pizzaID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            await Task.Run(() => order.Pizzas.Remove(pizza));
            await db.SaveChangesAsync();
            return await Task.Run(() => RedirectToAction("ReviewOrder", new { orderID = order.ID }));
        }

        public async Task<ActionResult> ChooseSide(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            var addSideVM = new SideViewModel() { Order = order };
            return await Task.Run(() => View(addSideVM));
        }

        public async Task<ActionResult> AddSide(int orderID, int sideID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            var side = await db.Sides.FindAsync(sideID);
            await Task.Run(() => order.AddSide(side));
            return await Task.Run(() => RedirectToAction("ChooseSide", new { orderID }));
        }

        public async Task<ActionResult> RemoveSide(int sidesPerOrderID)
        {
            var sidesPerOrder = await db.SidesPerOrder.FindAsync(sidesPerOrderID);
            var order = await db.Orders.FindAsync(sidesPerOrder.Order.ID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

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

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            var drinkVM = new DrinkViewModel(order);
            return await Task.Run(() => View(drinkVM));
        }

        public async Task<ActionResult> AddDrink(int orderID, int drinkID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            var drink = await db.Drinks.FindAsync(drinkID);
            await Task.Run(() => order.AddDrink(drink));
            return await Task.Run(() => RedirectToAction("ChooseDrink", new { orderID }));
        }

        public async Task<ActionResult> RemoveDrink(int drinksPerOrderID)
        {
            var drinksPerOrder = await db.DrinksPerOrder.FindAsync(drinksPerOrderID);
            var order = await db.Orders.FindAsync(drinksPerOrder.Order.ID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            if (drinksPerOrder.Amount > 1)
            {
                drinksPerOrder.Amount -= 1;
                await db.SaveChangesAsync();
            }
            else
            {
                order.DrinksPerOrder.Remove(drinksPerOrder);
                await db.SaveChangesAsync();
            }

            return await Task.Run(() => RedirectToAction("ReviewOrder", new { orderID = order.ID }));
        }

        public async Task<ActionResult> ReviewOrder(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            return await Task.Run(() => View(order));
        }

        public async Task<ActionResult> FinalizeOrder(int orderID)
        {
            var paymentDetails = new PaymentDetails { OrderID = orderID };
            var order = await db.Orders.FindAsync(orderID);

            if (order.Submitted)
                return await Task.Run(() => RedirectToAction("Order"));

            return await Task.Run(() => View(paymentDetails));
        }

        [HttpPost]
        public async Task<ActionResult> FinalizeOrder(PaymentDetails paymentDetails)
        {
            if(ModelState.IsValid)
            {
                var order = await db.Orders.FindAsync(paymentDetails.OrderID);

                if (order.Submitted)
                    return await Task.Run(() => RedirectToAction("Order"));

                order.PaymentDetails = paymentDetails;
                order.Submitted = true;
                await db.SaveChangesAsync();

                if (User.Identity.IsAuthenticated)
                {
                    var member = await db.Members.FindAsync(User.Identity.GetUserId());
                    var freePizzas = order.Pizzas.Where(p => p.Free).ToList();
                    
                    if(freePizzas.Any())
                    {
                        member.FreePizzasClaimed(freePizzas.Count());
                        await db.SaveChangesAsync();
                    }
                }
                
                return await Task.Run(() => RedirectToAction("ThankYou", new { orderID = order.ID }));
            }
            else
            {
                return await Task.Run(() => View(paymentDetails));
            }
        }

        public async Task<ActionResult> ThankYou(int orderID)
        {
            var order = await db.Orders.FindAsync(orderID);

            if (User.Identity.IsAuthenticated)
            {
                order.Submit();
                string memberID = User.Identity.GetUserId();
                var member = Member.FindOrCreate(db, memberID);
                member.Orders.Add(order);
                db.SaveChanges();
            }

            return await Task.Run(() => View(order));
        }

        public async Task<ActionResult> MyPoints()
        {
            if(User.Identity.IsAuthenticated)
            {
                var memberID = User.Identity.GetUserId();
                var member = await db.Members.FindAsync(memberID);

                db.SaveChanges();
                return await Task.Run(() => View(member));
            }
            else
            {
                throw new UnauthorizedAccessException("Guests don't have access to My Points page.");
            }
        }

        public async Task<ActionResult> FreePizza(int? pizzaID)
        {
            if(User.Identity.IsAuthenticated)
            {
                var member = await db.Members.FindAsync(User.Identity.GetUserId());

                if(member.GetPoints() > 0)
                {
                    var order = new Order();
                    db.Orders.Add(order);

                    Pizza pizza;
                    if(pizzaID.HasValue)
                    {
                        pizza = await db.Pizzas.FindAsync(pizzaID);
                    }
                    else
                    {
                        pizza = new Pizza { Inches = Size.Medium, Free = true };
                    }

                    order.Pizzas.Add(pizza);
                    await db.SaveChangesAsync();

                    var toppingVM = new ToppingViewModel { Pizza = pizza, Toppings = db.Toppings.ToList() };

                    return await Task.Run(() => View(toppingVM));
                }
                else
                {
                    throw new InvalidOperationException("You do not have enough points to claim a free pizza.");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Guests cannot claim free pizzas.");
            }
        }
    }
}