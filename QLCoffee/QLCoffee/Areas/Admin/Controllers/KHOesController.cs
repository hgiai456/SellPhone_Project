using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class KHOesController : Controller
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        // GET: Admin/KHOes
        public ActionResult Index()
        {
            var kHOes = db.KHOes.Include(k => k.NHACUNGCAP).Include(k => k.SANPHAM);
            return View(kHOes.ToList());
        }

        // GET: Admin/KHOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            return View(kHO);
        }

        // GET: Admin/KHOes/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/KHOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKho,MaSP,MaNCC,SoLuongTon,LastUpdate")] KHO kHO)
        {
            if (ModelState.IsValid)
            {
                db.KHOes.Add(kHO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", kHO.MaNCC);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", kHO.MaSP);
            return View(kHO);
        }

        // GET: Admin/KHOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", kHO.MaNCC);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", kHO.MaSP);
            return View(kHO);
        }

        // POST: Admin/KHOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKho,MaSP,MaNCC,SoLuongTon,LastUpdate")] KHO kHO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", kHO.MaNCC);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", kHO.MaSP);
            return View(kHO);
        }

        // GET: Admin/KHOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHO kHO = db.KHOes.Find(id);
            if (kHO == null)
            {
                return HttpNotFound();
            }
            return View(kHO);
        }

        // POST: Admin/KHOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHO kHO = db.KHOes.Find(id);
            db.KHOes.Remove(kHO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
