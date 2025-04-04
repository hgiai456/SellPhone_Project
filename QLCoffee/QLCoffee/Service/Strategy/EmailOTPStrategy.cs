using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Configuration;
using Twilio.TwiML.Messaging;

namespace QLCoffee.Service.Strategy
{
    public class EmailOTPStrategy : IOTPStrategy
    {             
        public void SendOTP(string to, string otp)
        {

            try
            {
                string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                string userName = ConfigurationManager.AppSettings["SmtpUser"];
                string password = ConfigurationManager.AppSettings["SmtpPass"];
                bool enableSs1 = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);


                //set SMPT client
                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(userName, password),
                    EnableSsl = enableSs1
                };

                //Create Email
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(fromEmail),
                    Subject = "Mã OTP của bạn",
                    Body = $"Mã OTP của bạn là: {otp}",
                    IsBodyHtml = true
                };

                mail.To.Add(to);

                //Send email
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Gửi email thất bại: " + ex.Message);
            }
        }
    }
}