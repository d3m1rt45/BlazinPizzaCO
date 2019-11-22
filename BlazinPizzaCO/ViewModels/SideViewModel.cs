using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.ViewModels
{
    public class SideViewModel
    {
        public SideViewModel()
        {
            using (var db = new BlazinContext())
            {
                this.Sides = db.Sides.ToList();
            }

            this.Order = new Order();
        }

        public Order Order { get; set; }
        public List<Side> Sides { get; set; }

    }
}