using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class ApprovedState : IOrderState
    {
        public void ChangeState(OrderContext order, string newState)
        {
            if (newState == "Đang giao")
            {
                order.SetState(new ShippingState());
            }
            else
            {
                throw new InvalidOperationException("Không thể chuyển trạng thái từ đã duyệt sang " + newState);
            }
        }

        public string GetStateName()
        {
            return "Đã duyệt";
        }
    }
}