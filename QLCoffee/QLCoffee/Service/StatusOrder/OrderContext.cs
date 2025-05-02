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
                case OrderStates.Pending:
                    return new PendingState();

                case OrderStates.Approved:
                    return new ApprovedState();

                case OrderStates.Shipping:
                    return new ShippingState();

                case OrderStates.Delivered:
                    return new DeliveredState();

                case OrderStates.Canceled:
                    return new CanceledState();
                default : throw new ArgumentException($"Trạng thái hóa đơn không hợp lệ: {state}");

            }
        }

        public void SetState(IOrderState state) {

            _state = state;
            Order.TrangThaiDH = state.GetStateName();

        }
        public void ChangeState(string newState) {

            _state.ChangeState(this, newState);         
        }
        public string Process()
        {
            return _state.HandleWareHouse(this);
        }


    }
}