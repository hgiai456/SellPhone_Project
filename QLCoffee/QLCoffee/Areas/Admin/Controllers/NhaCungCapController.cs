using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: NCC
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Index()
        {
            return View(database.NHACUNGCAPs.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string maNCC, string tenNCC, string diaChi, string soDT)
        {
            try
            {
                var nhacungcap = NhaCungCapFactory.Create(maNCC, tenNCC, diaChi, soDT);
                database.NHACUNGCAPs.Add(nhacungcap);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public ActionResult Details(string id)
        {
            return View(database.NHACUNGCAPs.Where(s => s.MaNCC == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            return View(database.NHACUNGCAPs.Where(s => s.MaNCC == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(string id, NHACUNGCAP nhacungcap)
        {
            try
            {
                database.Entry(nhacungcap).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Lỗi tại Property: {validationError.PropertyName} - {validationError.ErrorMessage}");
                        ModelState.AddModelError(validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return View(nhacungcap); // Trả lại view nếu có lỗi
            }
        }
        public ActionResult Delete(string id)
        {
            var nhacungcap = database.NHACUNGCAPs.FirstOrDefault(s => s.MaNCC == id);
            if (nhacungcap == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy
            }

            database.NHACUNGCAPs.Remove(nhacungcap);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(string id, NHACUNGCAP nhacungcap)
        {
            nhacungcap = database.NHACUNGCAPs.Where(s => s.MaNCC == id).FirstOrDefault();
            database.NHACUNGCAPs.Remove(nhacungcap);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}