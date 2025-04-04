using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class ProductMemento
    {
        public List<CartItem> Items { get; set; }

        public ProductMemento(List<CartItem> items)
        {
            Items = new List<CartItem>(items);
        }
    }
}