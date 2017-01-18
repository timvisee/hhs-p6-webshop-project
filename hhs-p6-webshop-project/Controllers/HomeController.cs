using Microsoft.AspNetCore.Mvc;
using hhs_p6_webshop_project.Models;
using System.Text;
using MailKit;
using System;
using System.Collections.Generic;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
namespace hhs_p6_webshop_project.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
              {
                try
                {

                    using (var client = new SmtpClient(new ProtocolLogger("smtp.log")))
                    {

                        client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                        client.Authenticate("miladintjuh@gmail.com", "Scouting1");

                        MimeMessage msg = new MimeMessage();
                        var from = c.Email.ToString();
                        StringBuilder sb = new StringBuilder();
                        msg.To.Add(new MailboxAddress("Honeymoonshop", "miladin@live.nl"));
                        msg.From.Add(new MailboxAddress("Contactformulier", from));

                        msg.Subject = "Contactformulier";
                        sb.Append("First name: " + c.Name);
                        sb.Append(Environment.NewLine);
                        sb.Append("Last name: " + c.Phone);
                        sb.Append(Environment.NewLine);
                        sb.Append("Email: " + c.Email);
                        sb.Append(Environment.NewLine);
                        sb.Append("Reference:" + c.Reference);
                        sb.Append(Environment.NewLine);
                        sb.Append("Comments: " + c.Comment);
                        msg.Body = new TextPart("plain")
                        {
                            Text = sb.ToString()
                        };
                        Console.WriteLine("The message is: " + sb);
                        client.Send(msg);
                        return View("Success");
                    }
                }


                catch (Exception)
                {
                    return View("Error");
                }
    }
     return View();
}
    }
}
