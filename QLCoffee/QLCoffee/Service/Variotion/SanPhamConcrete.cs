using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Variotion
{
    public class SanPhamConcrete : IPrototype<SanPhamConcrete> //ConcretePrototype
    {
        public string MaSP { get; set; }

        public string TenSP { get; set; }

        public int GiaSP { get; set; }
            
        public SanPhamConcrete(string maSP, string tenSP, int giaSP)
        {
            MaSP = maSP;
            TenSP = tenSP;
            GiaSP = giaSP;         

        }

        public SanPhamConcrete(SanPhamConcrete prototype)
        {
            MaSP = prototype.MaSP;
            TenSP = prototype.TenSP ;
            GiaSP = prototype.GiaSP;          

        }

        //Clone() to create a new object
        public virtual SanPhamConcrete Clone()
        {
            return new SanPhamConcrete(this);
        }
    }
    
    
}