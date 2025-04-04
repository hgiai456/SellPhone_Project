using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public interface IOrderState
    {
        void ChangeState(OrderContext  order, string newState); 
        string GetStateName();

    }
}