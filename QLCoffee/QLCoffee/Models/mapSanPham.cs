using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models
{
    public class mapSanPham
    {
        public bool UpdateImage(string idSanPham, string Image_url)
        {
            try
            {
                QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
                var sp = db.SANPHAMs.Find(idSanPham);
                sp.Image1 = Image_url;
                db.SaveChanges();
                return true;
            }

            catch
            { 
                return false;
            }
        }
        public bool UpdateImageProduct(string idPRODUCT, string Image1_url, string Image2_url, string Image3_url)
        {
            try
            {
                QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
                var sp = db.PRODUCTs.Find(idPRODUCT);
                sp.Img1 = Image1_url;
                sp.Img2 = Image2_url;
                sp.Img3 = Image3_url;

                db.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }

    }
}