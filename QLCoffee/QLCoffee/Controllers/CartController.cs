using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;
using QLCoffee.Models.ViewModel;
using QLCoffee.Service.LoaiSanPham;
using QLCoffee.Service.OrderDecorator;

namespace QLCoffee.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
        private static CartHistory history = new CartHistory();


        //Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }


        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        //Thêm sản phẩm vào giỏ hàng       
        //public ActionResult AddToCart(string id, int quantity = 1)
        //{

        //    var product = db.SANPHAMs.Find(id);

        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (quantity <= 0)
        //    {
        //        quantity = 1;
        //    }

        //    var cartService = GetCartService();
        //    cartService.GetCart().AddItem(product.MaSP, product.PRODUCT.Img1, product.TenSP, product.GiaSP, quantity);
        //    ///                                     
        //    return RedirectToAction("Index");
        //}
        public ActionResult AddToCart(string id, int quantity = 1, string dungLuong = "") // Mặc định là 128GB
        {
            var product = db.SANPHAMs.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (quantity <= 0)
            {
                quantity = 1;
            }

            // Sử dụng BaseProductOrder để đại diện cho sản phẩm gốc
            IOrder order = new BaseOrder(product.GiaSP);

            // Áp dụng Decorator theo dung lượng
            if (dungLuong == "1T")
            {
                order = new DungLuong1TDecorator(order);
            }
            else // Nếu không chọn hoặc chọn 128GB, mặc định dùng 128GB
            {
                order = new DungLuong128GBDecorator(order);
            }

            double finalPrice = order.GetTotal(); // Giá sau khi áp dụng Decorator
            ElectronicProduct epro = ElectronicProduct.CreateElectronicProduct(product.PRODUCT.LOAISANPHAM);
            if (epro != null)
            {
                epro.Price = finalPrice;
                double discount = 0;
                if (epro is Laptop laptop)
                {
                    discount = laptop.discount;
                }
                else if (epro is Phone phone)
                {
                    discount = phone.discount;
                }
                finalPrice *= (1 - discount);
            }
            var cartService = GetCartService();
            cartService.GetCart().AddItem(product.MaSP, product.PRODUCT.Img1, product.TenSP, (int)finalPrice, quantity);

            return RedirectToAction("Index");
        }
        // Xóa sản phẩm khỏi giỏ hàng
        public ActionResult RemoveFromCart(string id)
        {
            var cart = GetCart();

            // Lưu trạng thái cũ trước khi thay đổi
            history.Save(new ProductMemento(cart.Items));

            cart.RemoveItem(id);
            return RedirectToAction("Index");
        }
        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");

        }

        // Cập nhật số lượng sản phẩm
        [HttpPost]
        public ActionResult UpdateQuantity(string id, int quantity)
        {
            var cart = GetCart();

            // Lưu trạng thái cũ trước khi thay đổi
            history.Save(new ProductMemento(cart.Items));

            cart.UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        // Lấy giỏ hàng từ Session
        private Cart GetCart()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // Hoàn tác thay đổi giỏ hàng
        public ActionResult Undo()
        {
            var cart = GetCart();
            var previousState = history.Undo();

            if (previousState != null)
            {
                cart.Items = previousState.Items;
            }

            return RedirectToAction("Index");
        }

        //[HttpPost] 
        //public ActionResult AddToCart(string productId, int quantity)
        //{
        //    var product = db.SANPHAMs.FirstOrDefault(p => p.MaSP == productId);

        //    var cartItem = new CartItem
        //    {
        //        MaSP = product.MaSP,
        //        TenSP = product.TenSP,
        //        UnitPrice = product.GiaSP,
        //        Quantity = quantity
        //    };

        //    var cart = GetCart();
        //    cart.AddItem(cartItem);

        //    return View("Index");
        //}




        //public ActionResult RemoveFromCart(string productId)
        //{
        //    var cart = GetCart();
        //    cart.RemoveItem(productId);
        //    return RedirectToAction("Index");
        //}

        //public Cart GetCart()
        //{
        //    var cart = Session["Cart"] as Cart;
        //    if(cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;

        //    }
        //    return cart;
        //}

    }
}