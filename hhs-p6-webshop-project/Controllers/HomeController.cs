using Microsoft.AspNetCore.Mvc;
using hhs_p6_webshop_project.Models;
using System.Text;
using System.Net.Mail;
using mail
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
            // if (ModelState.IsValid)
            //  {
            //  try
            //  {


            SmtpClient client = new SmtpClient();
            // We use gmail as our smtp client
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("miladintjuh@gmail.com", "Scouting1");
            MailMessage msg = new MailMessage();
            MailAddress from = new MailAddress(c.Email.ToString());
            StringBuilder sb = new StringBuilder();
            msg.IsBodyHtml = false;
            msg.To.Add("miladin@live.nl");
            msg.From = from;
            msg.Subject = "Contact Us";
            sb.Append("First name: " + c.Name);
            sb.Append(Environment.NewLine);
            sb.Append("Last name: " + c.Phone);
            sb.Append(Environment.NewLine);
            sb.Append("Email: " + c.Email);
            sb.Append(Environment.NewLine);
            sb.Append("Reference:" + c.Reference);
            sb.Append(Environment.NewLine);
            sb.Append("Comments: " + c.Comment);
            msg.Body = sb.ToString();
            Console.WriteLine("The message is: " + sb);
            client.Send(msg);
            msg.Dispose();
            return View("Success");
        }
        //  catch (Exception)
        //   {
        //       return View("Error");
        //    }
    }
    // return View();
}
   // }
//}
    }
}