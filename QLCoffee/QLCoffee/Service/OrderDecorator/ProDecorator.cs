using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OrderDecorator
{
    public abstract class ProDecorator :IOrder
    {
        private IOrder order;

        protected ProDecorator(IOrder order)
        {
            this.order = order;
        }

        public virtual double GetTotal()
        {
            return order.GetTotal();
        }
    }
}