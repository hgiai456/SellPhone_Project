using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Variotion
{
    public class SanPhamChiTiet : SanPhamConcrete
    {

        public string Mau { get; set; }
        public string Size { get; set; }

        // Constructor thông thường
        public SanPhamChiTiet(string maSP, string tenSP, int giaSP, string mau, string size) : base(maSP, tenSP, giaSP)
        {
            Mau = mau;
            Size = size;
        }

        // Constructor sao chép prototype
        public SanPhamChiTiet(SanPhamChiTiet prototype) : base(prototype)
        {
            Mau = prototype.Mau;
            Size = prototype.Size;
        }

        // Clone() tạo bản sao mới cho SanPhamCaoCap
        public override SanPhamConcrete Clone()
        {
            return (SanPhamChiTiet)this.MemberwiseClone();
        }
    }
}