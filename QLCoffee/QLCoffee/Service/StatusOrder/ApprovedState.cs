using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace QLCoffee.Service.StatusOrder
{
    public class ApprovedState : IOrderState
    {
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();
        public void ChangeState(OrderContext order, string newState) //Khi trạng thái bằng đã duyệt thì chỉ có thể chuyển sang trạng thái đang giao 
        {
            if (newState == OrderStates.Shipping)
            {
                order.SetState(new ShippingState());
            }
            else
            {
                throw new InvalidOperationException("Không thể chuyển trạng thái từ đã duyệt sang " + newState);
            }
        }

        public string GetStateName()//Set giá trị cho trạng thái
        {
            return OrderStates.Approved;

        }
        //Hành động DoThis DoThat //Nếu như đơn hàng đã duyệt thì cập nhật lại kho
        public string HandleWareHouse(OrderContext order)
        {
            //Số lượng tồn kho sẽ lần lượt bị trừ theo số lượng sản phẩm có trong hóa đơn
            var chiTietHD = db.CHITIET_HOADON.Where(c => c.MaHD == order.Order.MaHD).ToList();
            
            if (chiTietHD == null)
            {
                return "Không tìm thấy chi tiết hóa đơn.";
            }
            string message = "";

            foreach (var item in chiTietHD)
            {
                var kho = db.KHOes.FirstOrDefault(k => k.MaSP == item.MaSP);
                if (kho != null && kho.SoLuongTon >= item.Soluong)
                {
                    kho.SoLuongTon = kho.SoLuongTon - item.Soluong;
                    kho.LastUpdate = DateTime.Now;
                }
                else
                {
                    message += $"Sản phẩm {item.SANPHAM.TenSP} không đủ số lượng trong kho. ";
                }
            }
            db.SaveChanges();
            return message == "" ? "Cập nhật kho thành công." : message;
        }
    }
}