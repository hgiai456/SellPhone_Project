using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Strategy
{
    public class OTPService
    {
        private IOTPStrategy _strategy;

        // Thiết lập strategy (Email hoặc SMS)
        public void SetStrategy(IOTPStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy), "Strategy không được null.");
        }

        // Gửi OTP với strategy đã chọn
        public void SendOTP(string to)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Vui lòng thiết lập Strategy trước khi gửi OTP.");
            }
            string otp = GenerateOTP();
            _strategy.SendOTP(to, otp);       
        }

        // Sinh mã OTP ngẫu nhiên
        public string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // OTP 6 số
        }
    }
}