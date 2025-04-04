using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OrderDecorator
{
    public class DungLuong1TDecorator : ProDecorator
    {
        private String name;
        public DungLuong1TDecorator(IOrder order) : base(order)
        {
            name = "1T";
        }

        public override double GetTotal()
        {
            return 10000000d+base.GetTotal();
        }
    }
}