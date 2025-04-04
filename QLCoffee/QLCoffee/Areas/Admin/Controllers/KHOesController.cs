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
    public class KHOesController : Controller//HG
    {
        private readonly IKhoRepository _khoRepository;
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        public KHOesController(IKhoRepository khoRepository)
        {
            _khoRepository = khoRepository;
        }
        // GET: Admin/KHOes
        public ActionResult Index()
        {
            var khoList = _khoRepository?.GetAll() ?? db.KHOes.Include(k => k.NHACUNGCAP).Include(k => k.SANPHAM).ToList();
            return View(khoList);
        }

        // GET: Admin/KHOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kho = _khoRepository?.GetById(id.Value) ?? db.KHOes.Find(id);
            if (kho == null) return HttpNotFound();
            return View(kho);
        }

        // GET: Admin/KHOes/Create
        public ActionResult Create()
        {
            LoadViewBags();
            return View();
        }       

        // POST: Admin/KHOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKho,MaSP,MaNCC,SoLuongTon,LastUpdate")] KHO kho)
        {
            if (ModelState.IsValid)
            {
                if (_khoRepository != null) _khoRepository.Add(kho);
                else
                {
                    db.KHOes.Add(kho);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            LoadViewBags(kho);
            return View(kho);
        }

        // GET: Admin/KHOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kho = _khoRepository?.GetById(id.Value) ?? db.KHOes.Find(id);
            if (kho == null) return HttpNotFound();

            LoadViewBags(kho);
            return View(kho);
        }

        // POST: Admin/KHOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKho,MaSP,MaNCC,SoLuongTon,LastUpdate")] KHO kho)
        {
            if (ModelState.IsValid)
            {
                if (_khoRepository != null) _khoRepository.Update(kho);
                else
                {
                    db.Entry(kho).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            LoadViewBags(kho);
            return View(kho);
        }

        // GET: Admin/KHOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var kho = _khoRepository?.GetById(id.Value) ?? db.KHOes.Find(id);
            if (kho == null) return HttpNotFound();
            return View(kho);
        }

        // POST: Admin/KHOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_khoRepository != null) _khoRepository.Delete(id);
            else
            {
                var kho = db.KHOes.Find(id);
                if (kho != null)
                {
                    db.KHOes.Remove(kho);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        private void LoadViewBags(KHO kho = null)
        {
            ViewBag.MaNCC = new SelectList(db.NHACUNGCAPs, "MaNCC", "TenNCC", kho?.MaNCC);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", kho?.MaSP);
        }
    }
}
