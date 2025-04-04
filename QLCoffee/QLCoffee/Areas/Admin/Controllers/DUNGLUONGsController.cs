using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;
using QLCoffee.Service.DungLuongFacade;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class DUNGLUONGsController : Controller
    {
        private readonly DungLuongService _dungLuongService;

        public DUNGLUONGsController()
        {
            _dungLuongService = new DungLuongService();
        }

        // GET: Admin/DUNGLUONGs
        public ActionResult Index()
        {
            return View(_dungLuongService.GetAll());
        }

        // GET: Admin/DUNGLUONGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dUNGLUONG = _dungLuongService.GetById(id.Value);
            if (dUNGLUONG == null) return HttpNotFound();
            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DUNGLUONGs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDungLuong,KichThuocDL")] DUNGLUONG dUNGLUONG)
        {
            if (ModelState.IsValid)
            {
                _dungLuongService.Create(dUNGLUONG);
                return RedirectToAction("Index");
            }
            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dUNGLUONG = _dungLuongService.GetById(id.Value);
            if (dUNGLUONG == null) return HttpNotFound();
            return View(dUNGLUONG);
        }

        // POST: Admin/DUNGLUONGs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDungLuong,KichThuocDL")] DUNGLUONG dUNGLUONG)
        {
            if (ModelState.IsValid)
            {
                _dungLuongService.Update(dUNGLUONG);
                return RedirectToAction("Index");
            }
            return View(dUNGLUONG);
        }

        // GET: Admin/DUNGLUONGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dUNGLUONG = _dungLuongService.GetById(id.Value);
            if (dUNGLUONG == null) return HttpNotFound();
            return View(dUNGLUONG);
        }

        // POST: Admin/DUNGLUONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _dungLuongService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
