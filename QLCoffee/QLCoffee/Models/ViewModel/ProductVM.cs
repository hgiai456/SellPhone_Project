using QLCoffee.Service.Variotion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QLCoffee.Models.ViewModel
{
    public class ProductVM
    {  
        public SANPHAM sanpham { get; set; }
        public int quantity { get; set; } 
        public int estimatedValue { get; set; } 
         
        // Thuộc tính hổ trợ lớp phân trang 
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } = 3;

        //8 san pham cung danh mục
        public string SearchTerm { get; set; }
        public List<SANPHAM> RelatedProduct { get; set; }

        public PagedList.IPagedList<SANPHAM> TopProduct { get; set; }

        //Danh sách biến thể
        public List<SanPhamConcrete> Varitions { get; set; }

        //Thuộc tính lưu trữ màu và kích thước được chọn

        public IEnumerable<SelectListItem> Colors { get; set; }
        public IEnumerable<SelectListItem> Sizes { get; set; }
        public string SelectedSize { get; set; }
        public string SelectedColor { get; set; }

    }
}