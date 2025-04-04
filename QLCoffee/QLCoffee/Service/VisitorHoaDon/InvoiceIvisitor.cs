using QLCoffee.Service.LoaiSanPham;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.VisitorHoaDon
{
    public class InvoiceIvisitor : IVisitor
    {
        public double GetDiscount(Laptop laptop)
        {
            return laptop.Discountitem;
        }

        public double GetDiscount(Phone phone)
        {
            return phone.Discountitem;
        }

        public int GetPrice(Laptop laptop)
        {
            return (int)laptop.Price;
        }

        public int GetPrice(Phone phone)
        {
            return (int)phone.Price;
        }
    }
}