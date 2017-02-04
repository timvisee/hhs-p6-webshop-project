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
        public void AboutInController()
        {
            Mock<HomeController> c = new Mock<HomeController>;
            c = new HomeController();

            var result = c.();

            //result moet een view zijn
            var viewResult = Assert.IsType<ViewResult>(result);

            //checken op correcte ViewData
            var message = viewResult.ViewData["Message"];
            Assert.Equal(message, "Contact | Honeymoon Shop");

        }


    }
}
