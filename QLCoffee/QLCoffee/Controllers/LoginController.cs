﻿ using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var check_TenDN = database.TAIKHOANs.Where(s => s.TenDN == taikhoan.TenDN).FirstOrDefault();
            try
            {
                if (check_TenDN == null) {
                    var check_PhanQuyen = database.TAIKHOANs.Where(s => s.PhanQuyen == taikhoan.PhanQuyen).FirstOrDefault();
                    if (check_PhanQuyen == null)
                    {
                        taikhoan.PhanQuyen = "C";
                    }

                    database.TAIKHOANs.Add(taikhoan);
                    database.SaveChanges();   /*Lưu lại data*/
                    return RedirectToAction("Login"); /*điều hướng về trang login*/
                }
                else
                {
                    ViewBag.ErrorMessage = "Tài khoản đã tồn tại.";
                    return View("Register", taikhoan);
                }
                
            }
            catch
            {
                ViewBag.ErrorMessage = "Trang đăng ký lỗi vui lòng thử lại.";
                return View("Register", taikhoan);
            }
        }
        public ActionResult Logout()
        {
            //Detele all session
            Session.Clear();          
            return RedirectToAction("TrangChu", "Home");
        }
    }
}