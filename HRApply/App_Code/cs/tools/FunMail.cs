using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace ApplyPromote.Tool
{
    public class FunMail
    {
        string fromAddress = "phoebe.learnlife@gmail.com";
        string fromPassword = "99990520";
        SmtpClient smtp;

        public void Execute()
        {
            smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress, fromPassword),
                Timeout = 20000
            };

        }

        protected Boolean SendMail(MailMessage mailObj)
        {            

            //SmtpClient SMTPServer = new SmtpClient("localhost");
            try
            {
                smtp.Send(mailObj);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
