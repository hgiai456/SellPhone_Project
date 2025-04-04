using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.OTP
{
    public interface IOTPStrategy
    {
        string GenerateOTP();

        bool ValidateOTP(string inputOTP,string generateOTP);
    }
}