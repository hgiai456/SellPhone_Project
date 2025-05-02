using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class PendingState : IOrderState //Trạng thái đang xử lý
    {
        public void ChangeState(OrderContext order , string newState)//Hàm đổi trạng thái 
        {
            if(newState == OrderStates.Approved) //Khi trạng thái mới bằng đã duyệt thì set thành trạng thái đã duyệt
            {
                order.SetState(new ApprovedState());

            }
            else if (newState == OrderStates.Canceled) //Khi trạng thái mới bằng đã hủy thì set thành trạng thái đã hủy
            {
                order.SetState(new CanceledState());

            }
            else
            {
                throw new InvalidOperationException("Không thể chuyển trạng thái từ đang xử lý sang " + newState); 
            }
        }

        public string GetStateName() //Set giá trị cho trạng thái
        {
            return OrderStates.Pending;
        }

        public string HandleWareHouse(OrderContext order) //Thực hiện hành động Dothis,Dothat nhưng không nằm trong trạng thái thay đổi số lượng tồn kho
        {
            // Không cần cập nhật kho khi đang giao hàng
            return "Đang xử lý, không cần cập nhật kho.";
        }
    }
}