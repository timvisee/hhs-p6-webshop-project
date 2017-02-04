using hhs_p6_webshop_project.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace hhs_p6_webshop_project_test
{
    public class contactTest
    {
        [Fact]
        public void ContactReturnViewTest()
        {
            //instantieer de controller
            HomeController c = new HomeController();
            var result = c.Contact();
            Console.WriteLine("Hoi");
            
            //result moet een view zijn
            var viewResult = Assert.IsType<ViewResult>(result);




        }

        [Fact]
        public void ContactViewDataTest()
        {
            HomeController c = new HomeController();
            var result = c.Contact();
            

            var viewResult = Assert.IsType<ViewResult>(result);
            var message = viewResult.ViewData["Message"];
            Console.WriteLine(viewResult.ViewData["Message"]);
            Assert.Equal(message, "Contact | Honeymoon Shop");
        }

    }
}
