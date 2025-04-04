using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Service.Strategy
{
    public interface IOTPStrategy
    {
        void SendOTP(string to, string otp);
    }
}