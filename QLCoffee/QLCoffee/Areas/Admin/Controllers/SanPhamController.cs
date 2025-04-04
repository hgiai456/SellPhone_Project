using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models.ViewModel;
using PagedList;
using System.Diagnostics;
using System.Data.Entity;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class DatabaseSingleton
    {
        private static QuanLyQuanCoffeeEntities _instance;
        private static readonly object _lock = new object();

        private DatabaseSingleton() { }

        public static QuanLyQuanCoffeeEntities Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new QuanLyQuanCoffeeEntities();
                    }
                    return _instance;
                }
            }
        }
    }
    public class SanPhamController : Controller
    {
        public ActionResult Index(string id, string searchTerm, int? minPrice, int? maxPrice, int? page, string sortOrder)
        {
            Debug.WriteLine(DatabaseSingleton.Instance.GetHashCode()); // Debug instance
            SANPHAM sp = DatabaseSingleton.Instance.SANPHAMs.Find(id);
            var model = new SearchProductVM();
            var sanphams = DatabaseSingleton.Instance.SANPHAMs.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                sanphams = sanphams.Where(p =>
                    p.TenSP.Contains(searchTerm) ||
                    p.PRODUCT.Desciption.Contains(searchTerm) ||
                    p.PRODUCT.LOAISANPHAM.TenLoaiSP.Contains(searchTerm));
            }

            if (minPrice.HasValue) sanphams = sanphams.Where(p => p.GiaSP >= minPrice.Value);
            if (maxPrice.HasValue) sanphams = sanphams.Where(p => p.GiaSP <= maxPrice.Value);

            switch (sortOrder)
            {
                case "name_asc":
                    sanphams = sanphams.OrderBy(p => p.TenSP);
                    break;
                case "name_desc":
                    sanphams = sanphams.OrderByDescending(p => p.TenSP);
                    break;
                case "price_asc":
                    sanphams = sanphams.OrderBy(p => p.GiaSP);
                    break;
                case "price_desc":
                    sanphams = sanphams.OrderByDescending(p => p.GiaSP);
                    break;
                default:
                    sanphams = sanphams.OrderBy(p => p.TenSP);
                    break;
            }

            int pageNumber = page ?? 1;
            int pageSize = 8;
            model.SanPhams = sanphams.ToPagedList(pageNumber, pageSize);
            model.SortOrder = sortOrder;
            return View(model);
        }
        public ActionResult Create()
        {

            var sanpham = new SANPHAM
            {
                NgaySX = DateTime.Now
            };
            ViewBag.IDPro = new SelectList(DatabaseSingleton.Instance.PRODUCTs, "IDPro", "NamePro", sanpham.IDPro);
            ViewBag.MaMau = new SelectList(DatabaseSingleton.Instance.MAUs, "MaMau", "TenMau", sanpham.MaMau);
            ViewBag.MaDungLuong = new SelectList(DatabaseSingleton.Instance.DUNGLUONGs, "MaDungLuong", "KichThuocDL", sanpham.MaDungLuong);
            return View(sanpham);
        }
        [HttpPost]
        public ActionResult Create(SANPHAM sanpham)
        {
            Debug.WriteLine(DatabaseSingleton.Instance.GetHashCode()); // Debug instance
            if (DatabaseSingleton.Instance.SANPHAMs.Any(sp => sp.MaSP == sanpham.MaSP))
            {
                ModelState.AddModelError("MaSP", "Khóa chính đã tồn tại!");
                return View(sanpham);
            }
            DatabaseSingleton.Instance.SANPHAMs.Add(sanpham);
            DatabaseSingleton.Instance.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            return View(DatabaseSingleton.Instance.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {


            var sanpham = new SANPHAM
            {
                NgaySX = DateTime.Now
            };
            ViewBag.IsEdit = true;
            ViewBag.IDPro = new SelectList(DatabaseSingleton.Instance.PRODUCTs, "IDPro", "NamePro");
            ViewBag.MaMau = new SelectList(DatabaseSingleton.Instance.MAUs, "MaMau", "TenMau");
            ViewBag.MaDungLuong = new SelectList(DatabaseSingleton.Instance.DUNGLUONGs, "MaDungLuong", "KichThuocDL");
            return View(DatabaseSingleton.Instance.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(string id, SANPHAM sanpham)
        {
            Debug.WriteLine(DatabaseSingleton.Instance.GetHashCode()); // Debug instance
            if (ModelState.IsValid)
            {
                DatabaseSingleton.Instance.Entry(sanpham).State = System.Data.Entity.EntityState.Modified;
                DatabaseSingleton.Instance.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }
        public ActionResult Delete(string id)
        {
            return View(DatabaseSingleton.Instance.SANPHAMs.Where(s => s.MaSP == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(string id,SANPHAM sanpham)
        {
            Debug.WriteLine(DatabaseSingleton.Instance.GetHashCode()); // Debug instance
            sanpham = DatabaseSingleton.Instance.SANPHAMs.Find(id);
            if (sanpham != null)
            {
                DatabaseSingleton.Instance.SANPHAMs.Remove(sanpham);
                DatabaseSingleton.Instance.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}