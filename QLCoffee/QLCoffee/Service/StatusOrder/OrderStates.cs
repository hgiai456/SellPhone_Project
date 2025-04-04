using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.StatusOrder
{
    public static class OrderStates
    {
         public const string Pending = "Đang xử lý";
         public const string Approved = "Đã duyệt";
         public const string Shipping = "Đang giao";
         public const string Delivered = "Đã giao";
         public const string Canceled = "Đã hủy";

    }
}