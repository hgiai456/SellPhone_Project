using PagedList;
using PagedList.Mvc;

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
        QuanLyQuanCoffeeEntities1 database = new QuanLyQuanCoffeeEntities1();
        public ActionResult Index(string searchString, int? page)
        {
            //Số dòng trên mỗi trang
            int pageSize = 10;

            //Số trang hiện tại (nếu không có thì mặt định là 1)
            int pageNumber = (page ?? 1);

            var danhsachHoaDon = database.HOADONs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                danhsachHoaDon = danhsachHoaDon.Where(h => h.MaHD.Contains(searchString) || h.SDT.Contains(searchString) || h.HoTenKH.Contains(searchString) || h.TenDN.Contains(searchString));

            }

            return View(danhsachHoaDon.OrderBy(h => h.MaHD).ToPagedList(pageNumber, pageSize));
    
        }
        public ActionResult Details(string id, SANPHAM sp)
        {
            var chitietHD = database.CHITIET_HOADON.Where(ct => ct.MaHD == id);
            //var MaSP = database.CHITIET_HOADON.Where(ct => ct.MaHD == id).Where(ct => ct.MaSP == sp.MaSP);

            return View(chitietHD);
        }
        [HttpPost]
        public JsonResult UpdateStatus(string id, string status)
        {
            var hoaDon = database.HOADONs.Find(id);
            if (hoaDon != null)
            {
                hoaDon.TrangThaiDH = status;
                database.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });

        } 

    }
}