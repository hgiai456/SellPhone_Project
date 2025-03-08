using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            return View(database.HOADONs.ToList());
        }
        public ActionResult Details(string id, SANPHAM sp)
        {
            var chitietHD = database.CHITIET_HOADON.Where(ct => ct.MaHD == id);
            //var MaSP = database.CHITIET_HOADON.Where(ct => ct.MaHD == id).Where(ct => ct.MaSP == sp.MaSP);

            return View(chitietHD);
        }
    }
}