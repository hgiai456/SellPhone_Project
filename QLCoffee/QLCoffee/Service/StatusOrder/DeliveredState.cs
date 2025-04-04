using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class DeliveredState : IOrderState
    {
        public void ChangeState(OrderContext order, string newState)
        {
            throw new InvalidOperationException("Không thể thay đổi trạng thái vì đơn hàng đã giao. ");
        }

        public string GetStateName()
        {
            return "Đã giao";
        }
    }
}