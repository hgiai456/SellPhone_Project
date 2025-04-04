using PagedList;
using PagedList.Mvc;
using QLCoffee.Models;
using QLCoffee.Service.LoaiSanPham;
using QLCoffee.Service.StatusOrder;
using QLCoffee.Service.VisitorHoaDon;
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
        public ActionResult Index(string searchString, int? page)
        {
            //Số dòng trên mỗi trang
            int pageSize = 9;

            //Số trang hiện tại (nếu không có thì mặt định là 1)
            int pageNumber = (page ?? 1);

            var danhsachHoaDon = database.HOADONs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                danhsachHoaDon = danhsachHoaDon.Where(h => h.MaHD.Contains(searchString) || h.SDT.Contains(searchString) || h.HoTenKH.Contains(searchString) || h.TenDN.Contains(searchString));

            }

            return View(danhsachHoaDon.OrderBy(h => h.MaHD).ToPagedList(pageNumber, pageSize));
    
        }
        //public ActionResult Details(string id, SANPHAM sp)
        //{
        //    var chitietHD = database.CHITIET_HOADON.Where(ct => ct.MaHD == id);
        //    //var MaSP = database.CHITIET_HOADON.Where(ct => ct.MaHD == id).Where(ct => ct.MaSP == sp.MaSP);

        //    return View(chitietHD);
        //}
        public ActionResult Details(string id, SANPHAM sp)
        {
            var chitietHDList = database.CHITIET_HOADON.Where(ct => ct.MaHD == id).ToList();
            Dictionary<ElectronicProduct, double> invoiceData = new Dictionary<ElectronicProduct, double>();
            InvoiceIvisitor invoiceVisitor = new InvoiceIvisitor();
            ViewBag.Khuyenmai = "Không Có Khuyến Mãi";
            foreach (var chitietHD in chitietHDList)
            {
                if (chitietHD?.SANPHAM?.PRODUCT?.LOAISANPHAM != null)
                {
                    ElectronicProduct electronicProduct = ElectronicProduct.CreateElectronicProduct(chitietHD.SANPHAM.PRODUCT.LOAISANPHAM);
                    if (electronicProduct is Item item)
                    {
                        item.Accept(invoiceVisitor);
                        invoiceData[electronicProduct] = invoiceVisitor.GetDiscount((dynamic)electronicProduct);//sử dụng dynamic bởi vì electronicProduct có thể là Laptop hoặc Phone
                                                                                                                //invoiceData[LOAISANPHAM.MapFromElectronicProduct(electronicProduct)] = ElectronicProduct.CreateElectronicProduct(chitietHD.SANPHAM.PRODUCT.LOAISANPHAM).discount;
                    }
                }

            }
            ViewBag.InvoiceData = invoiceData;
            return View(chitietHDList);
        }

        [HttpPost]
        public JsonResult UpdateStatus(string id, string status)
        {
            var hoaDon = database.HOADONs.Find(id);
            if(hoaDon == null)
            {
                return Json(new { success = false, message = "Không tìm thấy hóa đơn." });

            }           
            try
            {
                var orderContext = new OrderContext(hoaDon);
                orderContext.ChangeState(status);
                database.SaveChanges();
                return Json(new { success = true, message = "Cập nhật trạng thái hóa đơn thành công." });

            }
            catch (Exception ex) {
                return Json(new { success = false, message = ex.Message });
            }
            //if (hoaDon != null)
            //{
            //    hoaDon.TrangThaiDH = status;
            //    database.SaveChanges();

            //    return Json(new { success = true });
            //}

            //return Json(new { success = false });

        } 

    }
}