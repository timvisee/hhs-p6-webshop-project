using Microsoft.AspNetCore.Mvc;
using hhs_p6_webshop_project.Models;
using System.Text;
using MailKit;
using System;
using System.Collections.Generic;
using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project.Services.Containers;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace hhs_p6_webshop_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<SecureAppConfig> _secretConfig;
        private readonly ITransactionalEmailService _emailService;

        public HomeController(IOptions<SecureAppConfig> cfg, ITransactionalEmailService emailService)
        {
            _secretConfig = cfg;
            _emailService = emailService;
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
                _emailService.SendContactEmail(c);

                return View("Success");
            }
            return View();
        }
    }
}