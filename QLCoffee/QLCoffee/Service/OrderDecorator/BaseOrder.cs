using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OrderDecorator
{
    public class BaseOrder : IOrder
    {
        public BaseOrder(double price)
        {
            this.price = price;
        }
        public double price { get; set; }
        public double GetTotal()
        {
            return price;
        }   
    }
}