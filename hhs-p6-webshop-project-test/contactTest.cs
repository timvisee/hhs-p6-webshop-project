using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace hhs_p6_webshop_project_test
{
    public class contactTest
    {
        [Fact]
        public void ContactReturnViewTest()
        {
            //mock de parameters
            var cfgMock = new Mock<IOptions<SecureAppConfig>> ();
            var mailMock = new Mock<ITransactionalEmailService>();
            //instantieer de controller 
            HomeController c = new HomeController(null, null);
            var result = c.Contact();
            
            //result moet een view zijn
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void ContactViewDataTest()
        {
            HomeController c = new HomeController(null, null);
            var result = c.Contact();
            

            var viewResult = Assert.IsType<ViewResult>(result);
            var message = viewResult.ViewData["Message"];
            Assert.Equal(message, "Contact | Honeymoon Shop");
        }

    }
}
