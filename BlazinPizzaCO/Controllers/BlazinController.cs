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
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Order()
        {
            var basket = new Basket();
            return View(basket);
        }
    }
}