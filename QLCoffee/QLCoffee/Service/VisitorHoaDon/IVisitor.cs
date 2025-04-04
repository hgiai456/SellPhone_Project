using QLCoffee.Service.LoaiSanPham;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Service.VisitorHoaDon
{
    public interface IVisitor
    {
        double GetDiscount(Laptop laptop);
        int GetPrice(Laptop laptop);

        double GetDiscount(Phone phone);
        int GetPrice(Phone phone);
    }
}
