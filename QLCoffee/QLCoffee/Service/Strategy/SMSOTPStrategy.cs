using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace QLCoffee.Service.Strategy
{
    public class SMSOTPStrategy : IOTPStrategy
    {       
        public void SendOTP(string to, string otp)
        {
            try
            {
                string accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                string authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                string messagingServiceSid = ConfigurationManager.AppSettings["TwilioMessagingServiceSid"];
                TwilioClient.Init(accountSid, authToken);
                var message = MessageResource.Create(
                    body: $"Mã OTP của bạn là: {otp}",
                    from: new PhoneNumber(ConfigurationManager.AppSettings["TwilioPhoneNumber"]),
                    to: new PhoneNumber(to)
                );
               
            }
            catch (Exception ex)
            {
                throw new Exception("Gửi SMS thất bại: " + ex.Message);
            }
                   
        }
    }
}