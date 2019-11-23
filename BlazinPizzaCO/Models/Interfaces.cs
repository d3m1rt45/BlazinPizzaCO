using BlazinPizzaCO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazinPizzaCO.Interfaces
{
    public interface IExtra
    {
        int ID { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}