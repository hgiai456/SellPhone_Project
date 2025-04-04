using QLCoffee.Models;
using QLCoffee.Service.OrderDecorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.LoaiSanPham
{
	public abstract class ElectronicProduct
	{
		public IManageProduct manage;
        public  double Price { get; set; }
        public String MaLoaiSP { get; set;}
		public String LoaiSP { get; set; }
		public String mota { get; set; }
        public double discount { get; set; }
		public ElectronicProduct(IManageProduct manage)
		{
			this.manage = manage;
            this.PRODUCTs = new HashSet<PRODUCT>();

        }
        public virtual ICollection<PRODUCT> PRODUCTs { get; set; }
		public abstract void Displayinfo();
        public static ElectronicProduct CreateElectronicProduct(LOAISANPHAM loaiSanPham)
        {
            if (loaiSanPham == null || string.IsNullOrEmpty(loaiSanPham.TenLoaiSP))
            {
                return null; // Tránh lỗi NullReferenceException
            }

            string tenSP = loaiSanPham.TenLoaiSP.ToLower();

            if (tenSP.Contains("iphone"))
            {
                return new Phone(new ManagePhone());
            }
            else if (tenSP.Contains("macbook"))
            {
                return new Laptop(new ManageLaptop());
            }
            return null;
        }
    }
}