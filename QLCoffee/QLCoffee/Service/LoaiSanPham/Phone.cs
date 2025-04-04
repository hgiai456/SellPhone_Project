using QLCoffee.Models;
using QLCoffee.Service.OrderDecorator;
using QLCoffee.Service.VisitorHoaDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.LoaiSanPham
{
    public class Phone : ElectronicProduct,IOrder,Item
    {
        public Phone(IManageProduct manage) : base(manage)
        {
            LoaiSP = "IPhone";
            mota = "IPHONE VIP PRO";
            discount = 0.1;
        }

        public double Priceitem => Price;
        public double Discountitem => discount;

        public void Accept(IVisitor ivisitor)
        {
            ivisitor.GetPrice(this);
            ivisitor.GetDiscount(this);
        }

        public override void Displayinfo()
        {
            Console.WriteLine(LoaiSP);
        }
        public double GetTotal()
        {
            return Price;
        }
    }
}