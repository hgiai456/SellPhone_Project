using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class TAIKHOANMetadata
    {
        [DisplayName("TenDN")]
        [Required(ErrorMessage = "Error Empty")]

        public string TenDN { get; set; }
        [DisplayName("MatKhau")]
        [Required(ErrorMessage = "Error Empty")]
        public string MatKhau { get; set; }

        private string _PhanQuyen;
        [DisplayName("PhanQuyen")]
        [Required(ErrorMessage = "Error Empty")]
        [StringLength(1, ErrorMessage = "PhanQuyen save only 1 character")]
        public string PhanQuyen
        {
            get { return _PhanQuyen; }
            set { _PhanQuyen = value?.Trim(); } // Loại bỏ khoảng trắng khi gán giá trị
        }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
    }
    public class SANPHAMMetadata
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int GiaSP { get; set; }
        public Nullable<int> SoLuongSP { get; set; }
        public Nullable<System.DateTime> NgaySX { get; set; }
        public string Image1 { get; set; }
        public Nullable<int> MaMau { get; set; }
        public string IDPro { get; set; }
        public Nullable<int> SoLuongDaBan { get; set; }
    }
    public class PRODUCTMetadata
    {
        public string IDPro { get; set; }
        public string NamePro { get; set; }
        public string Desciption { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string MaLoaiSP { get; set; }
        
    }
}