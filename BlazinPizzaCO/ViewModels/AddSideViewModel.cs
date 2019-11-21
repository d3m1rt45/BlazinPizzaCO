using BlazinPizzaCO.DAL;
using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.ViewModels
{
    public class AddSideViewModel
    {
        public AddSideViewModel()
        {
            using (var db = new BlazinContext())
            {
                Sides = db.Sides.ToList();
            }
        }

        public Order Order { get; set; }
        public List<Side> Sides { get; set; }
    }
}