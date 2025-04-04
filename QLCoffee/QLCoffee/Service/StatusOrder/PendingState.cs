using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class PendingState : IOrderState
    {
        public void ChangeState(OrderContext order , string newState)
        {
            if(newState == OrderStates.Approved)
            {
                order.SetState(new ApprovedState());

            }
            else if (newState == OrderStates.Canceled)
            {
                order.SetState(new CanceledState());

            }
            else
            {
                throw new InvalidOperationException("Không thể chuyển trạng thái từ đang xử lý sang " + newState);
            }
        }

        public string GetStateName()
        {
            return OrderStates.Pending;
        }
    }
}