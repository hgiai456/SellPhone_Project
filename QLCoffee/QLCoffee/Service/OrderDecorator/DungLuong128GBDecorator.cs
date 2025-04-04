using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OrderDecorator
{
    public class DungLuong128GBDecorator : ProDecorator
    {
        public DungLuong128GBDecorator(IOrder order) : base(order)
        {
        }

        public override double GetTotal()
        {
            return 5000000d+base.GetTotal();
        }
    }
}