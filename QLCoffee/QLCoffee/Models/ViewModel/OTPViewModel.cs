using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class OTPViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
        public string UserName { get; set; }
      
        public string Email { get; set; }
             
        public string SDT { get; set; }

        public string Method { get; set; }
    }
}