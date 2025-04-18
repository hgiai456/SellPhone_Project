﻿using QLCoffee.Service.OrderDecorator;
using QLCoffee.Service.VisitorHoaDon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace QLCoffee.Service.LoaiSanPham
{
    public class Laptop : ElectronicProduct,IOrder,Item
    {
        
        public Laptop(IManageProduct manage) : base(manage)
        {
            LoaiSP = "MACBOOK";
            mota = "MACBOOK VIP PRO";
            discount = 0.05;
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