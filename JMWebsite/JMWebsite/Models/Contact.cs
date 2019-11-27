using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace JMWebsite.Models
{
    public class Contact
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        //public async static Task SendEmail(string email, string fName, string lName, string subject, string message)
        //{
        //    try
        //    {
        //        var _email = "Cyber-5@outlook.com";
        //        var _emailpass = "Qpwoeiru1440";
        //        //var _name = FirstName;
        //        MailMessage myMessage = new MailMessage();
        //        myMessage.To.Add(_email);
        //        myMessage.From = new MailAddress(email);
        //        myMessage.Subject = subject;
        //        myMessage.Body = message;
        //        myMessage.IsBodyHtml = true;

        //        using (SmtpClient smtp = new SmtpClient())
        //        {
        //            smtp.EnableSsl = true;
        //            smtp.Host = "smtp-mail.outlook.com";
        //            smtp.Port = 587;
        //            smtp.UseDefaultCredentials = false;
        //            smtp.Credentials = new NetworkCredential(_email, _emailpass);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.SendCompleted += (y, z) => { smtp.Dispose(); };
        //            await smtp.SendMailAsync(myMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}