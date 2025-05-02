using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QLCoffee.Models;

namespace QLCoffee.Service.StatusOrder
{
    public class CanceledState : IOrderState
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
        public void ChangeState(OrderContext order, string newState)
        {
            throw new InvalidOperationException("Không thể thay đổi trạng thái vì đơn hàng đã hủy. ");
        }
        //Set giá trị cho trạng thái
        public string GetStateName()
        {
            return OrderStates.Canceled;
        }
        //DoThis DoThat //Nếu như đơn hàng đã hủy thì cộng số lượng sản phẩm trong hóa đơn đã hủy vào kho
        public string HandleWareHouse(OrderContext order) 
        {
            var chiTietHD = db.CHITIET_HOADON.Where(c => c.MaHD == order.Order.MaHD).ToList(); 
            string message = "";

            foreach (var item in chiTietHD)
            {
                var kho = db.KHOes.FirstOrDefault(k => k.MaSP == item.MaSP);
                if (kho != null)
                {
                    kho.SoLuongTon = kho.SoLuongTon + item.Soluong;
                    kho.LastUpdate = DateTime.Now;
                    message += $"Sản phẩm {item.MaSP} đã được hoàn kho (+{item.Soluong}).\\n";
                }
                else
                {
                    // Nếu không tìm thấy kho, có thể thêm logic xử lý ở đây
                  message += $"Không tìm thấy sản phẩm {item.SANPHAM.TenSP} trong kho.";

                }
               
            }
            db.SaveChanges();
            return message == "" ? "Hóa đơn đã hủy, nhưng không có sản phẩm nào trong kho để cập nhật!\\n" : message;
        }
    }
}