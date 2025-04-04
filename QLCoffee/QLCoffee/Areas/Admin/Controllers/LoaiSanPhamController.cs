using QLCoffee.Models;
using QLCoffee.Service.LoaiSanPham;
using QLCoffee.Service.Menu;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: Admin/LoaiSanPham
        private readonly QuanLyQuanCoffeeEntities database=new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            //IMenuRenderer renderer;
            //// Chọn renderer dựa trên giá trị của style
            //if (style == "material")
            //{
            //    renderer = new MaterialRenderer();

            //}
            //else
            //{
            //    renderer = new BootstrapRenderer(); // Mặc định là BootstrapRenderer
            //}
            //renderer = new MaterialRenderer();
            //CategoryMenu menu = new ProductCategoryMenu(renderer);
            //string menuHtml = menu.Show();
            //ViewBag.MenuHtml = menuHtml;
            //ViewBag.MenuItems = menu.items;
            //if (renderer is MaterialRenderer renderer1)
            //{
            //    return PartialView("~/Views/Shared/View.cshtml", database.LOAISANPHAMs.ToList());
            //}
            //else
            //    return PartialView("~/Views/Shared/View1.cshtml", database.LOAISANPHAMs.ToList());
            ////return PartialView("~/Views/Shared/View.cshtml", database.LOAISANPHAMs.ToList());
            return View("~/Areas/Admin/Views/LoaiSanPham/Index.cshtml", database.LOAISANPHAMs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LOAISANPHAM loaisanpham)
        {
            if (ModelState.IsValid)
            {
                database.LOAISANPHAMs.Add(loaisanpham);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaisanpham);
        }
        public ActionResult Details(string id)
        {
            var loaiSanPham = database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault();
            ElectronicProduct product = ElectronicProduct.CreateElectronicProduct(loaiSanPham);
            if (product is Laptop laptop)
            {
                ViewBag.MoTa = laptop.mota;
                ViewBag.Discount = laptop.discount;
            }
            if (product is Phone phone)
            {
                ViewBag.MoTa = phone.mota;
                ViewBag.Discount = phone.discount;

            }
            return View("~/Areas/Admin/Views/LoaiSanPham/Details.cshtml", loaiSanPham);
        }
        public ActionResult Edit(string id)
        {
            var loaiSanPham = database.LOAISANPHAMs.FirstOrDefault(s => s.MaLoaiSP == id);
            if (loaiSanPham == null)
            {
                return HttpNotFound();
            }
            ElectronicProduct product = ElectronicProduct.CreateElectronicProduct(loaiSanPham);
            ViewBag.ProductInfo = product;
            return View("~/Areas/Admin/Views/LoaiSanPham/Edit.cshtml", loaiSanPham);
        }
        [HttpPost]
        public ActionResult Edit(string id, LOAISANPHAM loaisanpham)
        {
            database.Entry(loaisanpham).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id)

        {
            //return View(database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault());
            var loaiSanPham = database.LOAISANPHAMs.FirstOrDefault(s => s.MaLoaiSP == id);
            return View("~/Areas/Admin/Views/LoaiSanPham/Delete.cshtml", loaiSanPham);
        }
        [HttpPost]
        public ActionResult Delete(string id, LOAISANPHAM loaisanpham)
        {
            loaisanpham = database.LOAISANPHAMs.Where(s => s.MaLoaiSP == id).FirstOrDefault();
            database.LOAISANPHAMs.Remove(loaisanpham);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LoadPartialView(string style)
        {
            IMenuRenderer renderer;

            if (style == "material")
            {
                renderer = new MaterialRenderer();
            }
            else
            {
                renderer = new BootstrapRenderer();
            }

            CategoryMenu menu = new ProductCategoryMenu(renderer);
            string menuHtml = menu.Show();

            ViewBag.MenuHtml = menuHtml;
            ViewBag.MenuItems = menu.items;

            var data = database.LOAISANPHAMs.ToList();

            if (style == "material")
            {
                return PartialView("~/Views/Shared/View.cshtml", data);
            }
            else
            {
                return PartialView("~/Views/Shared/View1.cshtml", data);
            }
        }
    }
}