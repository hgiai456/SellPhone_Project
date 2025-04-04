using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Service.OrderDecorator
{
    public interface IOrder
    {
        double GetTotal();
    }
}
