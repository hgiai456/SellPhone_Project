using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class ShippingState : IOrderState
    {
        public void ChangeState(OrderContext order, string newState)
        {
            if (newState == OrderStates.Delivered)
            {
                order.SetState(new DeliveredState());

            }
            else
            {
                throw new InvalidOperationException("Không thể chuyển trạng thái từ Đang giao sang " + newState);
            }
        }

        public string GetStateName()
        {
            return OrderStates.Shipping;
        }
    }
}