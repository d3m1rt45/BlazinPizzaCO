using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.ViewModels
{
    public class ToppingViewModel
    {
        public List<Topping> Toppings { get; set; }
        public Pizza Pizza { get; set; }
    }
}