 using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLCoffee.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        QuanLyQuanCoffeeEntities database = new QuanLyQuanCoffeeEntities();
        public ActionResult Login()
        {
            return View();
        }
        //POST: Account Login
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AuthenLogin(TAIKHOAN taikhoan)
        {
            try
            {
                var check_ID = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
                var check_Pass = database.TAIKHOANs.Where(s => s.MatKhau == taikhoan.MatKhau).FirstOrDefault();
                var user = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
                if (check_ID == null || check_Pass == null)
                {
                    if (check_ID == null)
                    {

                        ViewBag.ErrorID = "Tên Đăng Nhập Sai";

                    }
                    if (check_Pass == null)
                    {

                        ViewBag.ErrorPass = "Mật Khẩu Đăng Nhập Sai";

                    }
                    return View("Login");
                }
                else
                {
                    if (user.PhanQuyen == "A")
                    {
                        //Luu thong tin xac thuc cua nguoi dung vao cookie
                        Session["TenDN"] = user.TenDN;
                        Session["PhanQuyen"] = user.PhanQuyen;
                        return RedirectToAction("Index", "TaiKhoan", new {area = "Admin"});
                    }
                    else if (user.PhanQuyen == "C")
                    {
                        Session["TenDN"] = user.TenDN;
                        Session["PhanQuyen"] = user.PhanQuyen;
                        return RedirectToAction("TrangChu", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Tên đăng nhập không tồn tại";
                        return View("Login");
                    }
                }
            } 
            catch
            {
                return View("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AuthenRegister(TAIKHOAN taikhoan)
        {
            try
            {
                var check_PhanQuyen = database.TAIKHOANs.Where(s => s.PhanQuyen == taikhoan.PhanQuyen).FirstOrDefault();
                if (check_PhanQuyen == null)
                {
                    taikhoan.PhanQuyen = "C";
                }

                database.TAIKHOANs.Add(taikhoan); 
                database.SaveChanges();   /*Lưu lại data*/
                return RedirectToAction("Login"); /*điều hướng về trang login*/

            }
            catch
            { 
                return View("Register") ;
            }
        }
        public ActionResult Logout()
        {
            //Detele all session
            Session.Clear();
            Session.Abandon();

            //Detele cookie
            if(Request.Cookies["TenDN"] != null)
            {
                var cookie = new HttpCookie("TenDN")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("TrangChu", "Home");
        }
    }
}