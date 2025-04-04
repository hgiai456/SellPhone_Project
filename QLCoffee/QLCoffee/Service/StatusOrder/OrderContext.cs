using Microsoft.Ajax.Utilities;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public class OrderContext
    {
        private IOrderState _state;
        public HOADON Order {  get; set; }

        public OrderContext(HOADON order) {
        
            Order = order;
            _state = GetStateFromDb(order.TrangThaiDH);
        
        }

        private IOrderState GetStateFromDb(string state) //Nếu như 
        {
            switch (state)
            {
                case "Đang xử lý": return new PendingState();

                case "Đã duyệt": return new ApprovedState();

                case "Đang giao": return new ShippingState();

                case "Đã giao": return new DeliveredState();

                case "Đã hủy": return new CanceledState();

                default : throw new ArgumentException("Trạng thái hóa đơn không hợp lệ.");

            }
                                
        }

        public void SetState(IOrderState state) {

            _state = state;
            Order.TrangThaiDH = state.GetStateName();

        }
        public void ChangeState(string newState) {

            _state.ChangeState(this, newState);         
        }


    }
}