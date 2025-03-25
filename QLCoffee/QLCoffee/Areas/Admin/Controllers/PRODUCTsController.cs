
using PagedList;
using PagedList.Mvc;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;

using System.IO; //Thu vien nhap xuat file

namespace QLCoffee.Areas.Admin.Controllers
{
    public class PRODUCTsController : Controller
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        // GET: Admin/PRODUCTs
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 4;

            //Số trang hiện tại (nếu không có thì mặt định là 1)
            int pageNumber = (page ?? 1);

            var danhsachPro = db.PRODUCTs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                danhsachPro = danhsachPro.Where(tk => tk.IDPro.Contains(searchString) || tk.NamePro.Contains(searchString));
            }

            return View(danhsachPro.OrderBy(tk => tk.IDPro).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/PRODUCTs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPro,NamePro,Desciption,Img1,Img2,Img3,MaLoaiSP")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP", pRODUCT.MaLoaiSP);
            return View(pRODUCT);
        }
        public ActionResult UploadProduct()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProduct([Bind(Include = "IDPro,NamePro,Desciption,Img1,Img2,Img3,MaLoaiSP,UploadImage,UploadImage1,UploadImage2")] PRODUCT product)
        {
            if (ModelState.IsValid)
            {
                if(product.UploadImage != null)
                {
                    string path = "~/Content/Image/";
                    string fileName = Path.GetFileName(product.UploadImage.FileName);
                    product.Img1 = path + fileName;
                    product.UploadImage.SaveAs(Path.Combine(Server.MapPath(path),fileName));
                }
                if (product.UploadImage1 != null)
                {
                    string path = "~/Content/Image/";
                    string fileName = Path.GetFileName(product.UploadImage1.FileName);
                    product.Img2 = path + fileName;
                    product.UploadImage1.SaveAs(Path.Combine(Server.MapPath(path), fileName));
                }
                if (product.UploadImage2 != null)
                {
                    string path = "~/Content/Image/";
                    string fileName = Path.GetFileName(product.UploadImage2.FileName);
                    product.Img3 = path + fileName;
                    product.UploadImage2.SaveAs(Path.Combine(Server.MapPath(path), fileName));
                }

                db.PRODUCTs.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP", product.MaLoaiSP); //Truy vấn khóa phụ để tìm mã loại sản phẩm
            return View(product);
        }

        // GET: Admin/PRODUCTs/Edit/5
        public ActionResult EditProduct(string id)
        {
                                   
            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP");

            return View(db.PRODUCTs.Where(s => s.IDPro == id).FirstOrDefault());
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "IDPro,NamePro,Desciption,Img1,Img2,Img3,MaLoaiSP,UploadImage,UploadImage1,UploadImage2")] PRODUCT product)
        {
            if (ModelState.IsValid)
            {
                
                var existingProduct = db.PRODUCTs.Find(product.IDPro);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }
                
                string path = "~/Content/Image/";
                // Kiểm tra và cập nhật ảnh mới nếu có
                if (product.UploadImage != null && product.UploadImage.ContentLength > 0)
                {
                    
                    string fileName = Path.GetFileName(product.UploadImage.FileName);
                    string fullPath = Path.Combine(Server.MapPath(path), fileName);                    
                    product.UploadImage.SaveAs(Path.Combine(fullPath));
                    product.Img1 = path + fileName;
                }

                if (product.UploadImage1 != null && product.UploadImage1.ContentLength > 0)
                {
                    
                    string fileName = Path.GetFileName(product.UploadImage1.FileName);
                    string fullPath = Path.Combine(Server.MapPath(path), fileName);                   
                    product.UploadImage1.SaveAs(fullPath);
                    product.Img2 = path + fileName;

                }

                if (product.UploadImage2 != null && product.UploadImage2.ContentLength > 0)
                {

                    string fileName = Path.GetFileName(product.UploadImage2.FileName);
                    string fullPath = Path.Combine(Server.MapPath(path), fileName);
                    product.UploadImage2.SaveAs(fullPath);
                    product.Img3 = path + fileName;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
               
            }

            ViewBag.MaLoaiSP = new SelectList(db.LOAISANPHAMs, "MaLoaiSP", "TenLoaiSP", product.MaLoaiSP);
            return View(product);
        }

        // GET: Admin/PRODUCTs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult UploadImage()
        //{

        //}

    }
}
