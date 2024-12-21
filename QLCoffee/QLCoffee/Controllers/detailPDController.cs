using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;
using QLCoffee.Models.ViewModel;



namespace QLCoffee.Controllers
{
    public class detailPDController : Controller
    {
        QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
        // GET: detailPD
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DetailProduct(string id)
        //{
        //    if (id != null)
        //    {
        //        SANPHAM sp = db.SANPHAMs.FirstOrDefault(p => p.MaSP == id);
        //        ProductVM detailProductVM = new ProductVM
        //        {
        //            //get information of table Product
        //            MaSP = sp.MaSP,
        //            GiaSP = sp.GiaSP,
        //            SoLuongSP = sp.SoLuongSP,
        //            SoLuongDaBan = sp.SoLuongDaBan,
        //            //get information of table DetailProduct
        //            IDPro = sp.IDPro,
        //            NamePro = sp.PRODUCT.NamePro,
        //            Img1 = sp.PRODUCT.Img1,
        //            Img2 = sp.PRODUCT.Img2,
        //            Img3 = sp.PRODUCT.Img3,
        //            //get information of table Category
        //            MaLoaiSP = sp.PRODUCT.MaLoaiSP,
        //            TenLoaiSP = sp.PRODUCT.LOAISANPHAM.TenLoaiSP,
        //            //get information of table Color
        //            MaMau = sp.MAU.MaMau,
        //            TenMau = sp.MAU.TenMau,
        //            RGB = sp.MAU.RGB,

        //            RelatedProduct = RelatedProduct(sp.PRODUCT.MaLoaiSP)
        //        };
        //        return View(detailProductVM);
        //    }
        //    return Content("Missing ID!");
        //}
        //public List<ProductVM> RelatedProduct(string catid = "ML01")
        //{
        //    if (catid != null)
        //    {
        //        return new List<ProductVM>();
        //    }
        //    List<SANPHAM> prodetail = db.SANPHAMs.Where(p => p.PRODUCT.MaLoaiSP == catid).ToList();
        //    List<ProductVM> relatedProducts = prodetail.Select(
        //            sp => new ProductVM
        //            {
        //                //get information of table Product
        //                MaSP = sp.MaSP,
        //                GiaSP = sp.GiaSP,
        //                SoLuongSP = sp.SoLuongSP,
        //                SoLuongDaBan = sp.SoLuongDaBan,
        //                //get information of table DetailProduct
        //                IDPro = sp.IDPro,
        //                NamePro = sp.PRODUCT.NamePro,
        //                Img1 = sp.PRODUCT.Img1,
        //                Img2 = sp.PRODUCT.Img2,
        //                Img3 = sp.PRODUCT.Img3,
        //                //get information of table Category
        //                MaLoaiSP = sp.PRODUCT.MaLoaiSP,
        //                TenLoaiSP = sp.PRODUCT.LOAISANPHAM.TenLoaiSP,
        //                //get information of table Color
        //                MaMau = sp.MAU.MaMau,
        //                TenMau = sp.MAU.TenMau,
        //                RGB = sp.MAU.RGB
        //            }).ToList();
        //                return relatedProducts;
        //}
        //public ActionResult ProductByCategory(string catid = "ML01")
        //{
        //    return PartialView(RelatedProduct(catid));
        //}
    }
}