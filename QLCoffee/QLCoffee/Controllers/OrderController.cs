using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;
using QLCoffee.Models.ViewModel;

namespace QLCoffee.Controllers
{

    public class OrderController : Controller
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        private CartService GetCartService()
        {
            return new CartService(Session);
        }
        private LoginService GetLoginService()
        {
            return new LoginService(Session);
        }

        public ActionResult Checkout()
        {
            //Kiểm tra giỏ hàng trong session 
            //Nếu giỏ hàng rỗng thì chuyển về trang chủ 

            var cart = GetCartService().GetCart();

            var login = GetLoginService().GetUserName();

            if(cart == null || !cart.Items.Any())
            {
                return RedirectToAction("TrangChu", "Home");
            }
            if(login == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //Xác thực người dùng đã đăng nhập chưa, nếu chưa thì chuyển hướng tới đăng nhập
            var user = db.TAIKHOANs.SingleOrDefault(u => u.TenDN == User.Identity.Name);
           
            //Lấy thông tin khách hàng từ cơ sở dữ liệu
            //Nếu có rồi thì lấy địa chỉ khách hàng và gán vào GhiChu của CheckOutVM
            var model = new CheckoutVM
            {
                //Tạo dữ liệu cho CheckoutVM 
                CartItems = cart.Items,//Lấy danh sách các sản phẩm trong giỏ hàng
                TongGiaTriHD = cart.TotalValue(),//Tổng giá trị các mặt hàng
                NgayTD = DateTime.Now,
                TenDN = login,               

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Checkout(CheckoutVM model)
        {
            var cart = GetCartService().GetCart();
            //Khai bao Session TenDN
            var login = GetLoginService().GetUserName();

            //Tạo mã hóa đơn mới
            string lastOrderId = db.HOADONs.OrderByDescending(o => o.MaHD).Select(o => o.MaHD).FirstOrDefault();

            string newOrderId = GenerateNewOrderId(lastOrderId);

            

            if (ModelState.IsValid)
            {

                //Khai bao Session Cart
               
                //Nếu giỏ hàng rổng sẽ điều hướng tới trang Home
                if (cart == null || !cart.Items.Any())
                {
                    return RedirectToAction("TrangChu", "Home");
                }


                //Nếu người dùng chưa đăng nhập sẽ điêu hướng tới trang đăng nhập
                var user = db.TAIKHOANs.SingleOrDefault(u => u.TenDN == User.Identity.Name);

                //Lấy hóa đơn lớn nhất hiện tại
                

               
               


                //Tạo đơn hàng và chi tiết hóa đơn liên quan
                var order = new HOADON
                {
                    MaHD = newOrderId,
                    TenDN = login,
                    NgayTD = DateTime.Now.Date,
                    TongGiaTriHD = cart.TotalValue(),
                    TrangThaiDH = "Đang xử lý",
                    GhiChu = model.DiaChiDH,
                    HoTenKH = model.HoTenKH,
                    SDT = model.SoDienThoai,

                    CHITIET_HOADON = cart.Items.Select(item => new CHITIET_HOADON
                    {
                        MaHD = newOrderId,
                        MaSP = item.MaSP,
                        Soluong = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                        ///Continue
                    }).ToList()

                };
                //Lưu thông tin đơn hàng vào CSDL
                db.HOADONs.Add(order);
                db.SaveChanges();
                //Xóa giỏ hàng sau khi đặt hàng thành công
                GetCartService().ClearCart();
                //Điều hướng đến trang xác nhận đơn hàng
                return RedirectToAction("OrderSuccess", new { id = order.MaHD });

            }
            //Nếu ModelState không hợp lệ, nạp dữ liệu cho giỏ hàng
            model.CartItems = cart.Items;
            model.TongGiaTriHD = cart.TotalValue();

            return View(model);
            
            
        }
        //Hàm sinh mã hóa đơn mới
        private string GenerateNewOrderId(string lastOrderId)
        {
            if (string.IsNullOrEmpty(lastOrderId))
            {
                return "HD0001";
            }
            int lastNumber = int.Parse(lastOrderId.Substring(2));//Chuyển đổi từ sau ký tự "HD" (vị trí số 2) thành kiểu int
            int newNumber = lastNumber + 1;

            return "HD" + newNumber.ToString("D4");
        }
        //Xử lý xác nhận đơn hàng
        public ActionResult OrderSuccess(string id)
        {
            //Lấy thông tin đơn hàng theo mã đơn hàng
            var order = db.HOADONs.Include("CHITIET_HOADON").SingleOrDefault(o => o.MaHD == id);
            //Nếu không tìm thấy đơn hàng, điều hướng về trang chủ
            if(order == null)
            {
                return RedirectToAction("TrangChu", "Home");

            }

            var model = new CheckoutVM
            {

                TenDN = order.TenDN,
                NgayTD = order.NgayTD,
                TongGiaTriHD = order.TongGiaTriHD,
                DiaChiDH = order.GhiChu,
                TrangThaiDH = order.TrangThaiDH,
                CartItems = order.CHITIET_HOADON.Select(ct => new CartItem {
                    MaSP = ct.MaSP,
                    Quantity = ct.Soluong,
                    UnitPrice = ct.UnitPrice,                    
                    
                }).ToList()
               
            };
            return View(model);
        }

    }
}