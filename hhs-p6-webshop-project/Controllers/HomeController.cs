using Microsoft.AspNetCore.Mvc;
using hhs_p6_webshop_project.Models;
using System.Text;
using MailKit;
using System;
using System.Collections.Generic;
using hhs_p6_webshop_project.App.Config;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace hhs_p6_webshop_project.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<SecureAppConfig> _secretConfig;

        public HomeController(IOptions<SecureAppConfig> cfg)
        {
            _secretConfig = cfg;
        }

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                if (Beun.Mail.MailClient.ApiKey == null)
                    Beun.Mail.MailClient.ApiKey = _secretConfig.Value.SparkpostApiKey;

                Beun.Mail.MailClient.SendContactMail(c.Name, c.Email, c.Reference, c.Comment, c.Phone);

                return View("Success");
            }
            return View();
        }
    }
}