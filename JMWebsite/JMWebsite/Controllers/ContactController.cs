using JMWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JMWebsite.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
        
        //Captcha
        [HttpPost]
        public ActionResult ValidateCaptcha()
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LeujBQUAAAAAOu4vK5t5eOINCA-Uqu2ZwwSDQU1";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return View();

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        ViewBag.Message = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        ViewBag.Message = "The secret parameter is invalid or malformed.";
                        break;

                    case ("missing-input-response"):
                        ViewBag.Message = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        ViewBag.Message = "The response parameter is invalid or malformed.";
                        break;

                    default:
                        ViewBag.Message = "Error occured. Please try again";
                        break;
                }
            }
            else
            {
                ViewBag.Message = "Valid";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("Cyber-5@outlook.com"));  // replace with valid value 
                message.From = new MailAddress("cyber5johnmicheals@gmail.com");  // replace with valid value
                message.Subject = "Contact Request";
                message.Body = string.Format(body, model.FirstName, model.Email, model.Phone, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "cyber5johnmicheals@gmail.com",  // replace with valid value
                        Password = "Qpwoeiru1440"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        //public ActionResult Sent()
        //{
        //    return View();
        //}


        //public ActionResult SendEmail()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> SendEmail(Contact model)
        //{
        //    await SendEmail(model.Email, model.FirstName, model.LastName, "JM Contact Request", model.Message);
        //    return View("Sent");
        //}

        //[HttpPost]
        //public ActionResult Contact(Contact e)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        //prepare email
        //        var toAddress = "Cyber5johnmicheals@gmail.com";
        //        var fromAddress = e.Email.ToString();
        //        var subject = "JM Contact Request from " + e.FirstName + " " + e.LastName;
        //        var message = new StringBuilder();
        //        message.Append("First Name: " + e.FirstName + "\n");
        //        message.Append("Last Name: " + e.LastName + "\n");
        //        message.Append("Email: " + e.Email + "\n");
        //        message.Append("Phone: " + e.Phone + "\n\n");
        //        message.Append(e.Message);

        //        //start email Thread
        //        var tEmail = new Thread(() => SendEmail(toAddress, fromAddress, subject, message.ToString()));
        //        tEmail.Start();
        //    }
        //    return View();
        //}


        //public void SendEmail(string toAddress, string fromAddress, string subject, string message)
        //{
        //    try
        //    {
        //        using (var mail = new MailMessage())
        //        {
        //            const string email = "cyber5@outlook.com";
        //            const string password = "Qpwoeiru1440";

        //            var loginInfo = new NetworkCredential(email, password);


        //            mail.From = new MailAddress(fromAddress);
        //            mail.To.Add(new MailAddress(toAddress));
        //            mail.Subject = subject;
        //            mail.Body = message;
        //            mail.IsBodyHtml = true;

        //            try
        //            {
        //                using (var smtpClient = new SmtpClient(
        //                                                 "smtp-mail.outlook.com", 587))
        //                {
        //                    smtpClient.EnableSsl = true;
        //                    smtpClient.UseDefaultCredentials = false;
        //                    smtpClient.Credentials = loginInfo;
        //                    smtpClient.Send(mail);
        //                }

        //            }

        //            finally
        //            {
        //                //dispose the client
        //                mail.Dispose();
        //            }

        //        }
        //    }
        //    catch (SmtpFailedRecipientsException ex)
        //    {
        //        foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
        //        {
        //            var status = t.StatusCode;
        //            if (status == SmtpStatusCode.MailboxBusy ||
        //                status == SmtpStatusCode.MailboxUnavailable)
        //            {
        //                Response.Write("Delivery failed - retrying in 5 seconds.");
        //                System.Threading.Thread.Sleep(5000);
        //            }
        //            else
        //            {
        //                //Response.Write("Failed to deliver message to {0}",
        //                //                  t.FailedRecipient);
        //            }
        //        }
        //    }
        //    catch (SmtpException Se)
        //    {
        //        // handle exception here
        //        Response.Write(Se.ToString());
        //    }

        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.ToString());
        //    }
        //}


    }
}