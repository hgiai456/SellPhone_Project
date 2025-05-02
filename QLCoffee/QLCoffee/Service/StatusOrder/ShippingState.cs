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

            }else if (newState == OrderStates.Canceled)
            {
                order.SetState(new CanceledState());
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

        public string HandleWareHouse(OrderContext order)
        {
            // Không cần cập nhật kho khi đang giao hàng
            return "Đang giao hàng, không cần cập nhật kho.";
        }
    }
}