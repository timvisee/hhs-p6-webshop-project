using System;
using System.Linq;
using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Models.AppointmentModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using hhs_p6_webshop_project_test.Roderick;

namespace hhs_p6_webshop_project_test.Roderick
{
    public class AppointmentControllerTest
    {

        [Fact]
        public void TestIndex_NotLoggedIn()
        {
            var controller = Tools.CreateControllerInstance();
            
            //Execute the method
            var result = controller.Index();

            //Check the result
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void TestIndex_LoggedIn()
        {
            var controller = Tools.CreateControllerInstance(true); //true: user is logged in
            
            //Execute the method
            var result = controller.Index();

            //Check the result
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestDetails_NullParameter()
        {
            var controller = Tools.CreateControllerInstance();
        
            //Execute the method
            var result = controller.Details(null);

            //Check the result
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void TestDetails_InvalidId()
        {
            var controller = Tools.CreateControllerInstance();
        
            //Execute the method
            var result = controller.Details(-1);

            //Check the result
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void TestDetails_ValidId_NotLoggedIn()
        {
            var controller = Tools.CreateControllerInstance();
        
            //Execute the method with the ID of the first appointment
            var result = controller.Details(Tools.CreateTestAppointments().FirstOrDefault().ID);

            //Check the result, we should have gotten a Forbidden error
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void TestDetails_ValidId_LoggedIn()
        {
            var controller = Tools.CreateControllerInstance(true); //true: user is logged in
        
            //Execute the method with the ID of the first appointment
            var result = controller.Details(Tools.CreateTestAppointments().FirstOrDefault().ID);

            //Check the result, we should have gotten a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestCreate_NullParameter_Get()
        {
            var controller = Tools.CreateControllerInstance();
        
            //Execute the method with the ID of the first appointment
            var result = controller.Create(null, null);

            //Check the result, we should have gotten a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TestCreate_NullParameter_Post()
        {
            var controller = Tools.CreateControllerInstance();
        
            //Execute the method with the ID of the first appointment
            var result = controller.Create(null, "dressname", "dresscolor");

            //Check the result, we should have gotten a BadRequest
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void TestCreate_Valid_Post()
        {
            var httpContext = Tools.MockHttpContext(false);
            var dbContext = Tools.MockTestDatabaseContext();
            var config = Tools.MockTestConfig();
            var service = Tools.MockEmailService();

            var controller = new AppointmentsController(dbContext, config, service)
            {
                ControllerContext = new ControllerContext()
                {
                    //Override http context, to return the proper user
                    HttpContext = httpContext
                }
            };

            Appointment newAppointment = new Appointment()
            {
                AppointmentDateTime = DateTime.Now,
                Confirmation = false,
                DateMarried = DateTime.Now,
                ID = 9,
                Mail = "test@rick-soft.com",
                Name = "Test",
                Phone = "00000000000"
            };
            
            //Execute the method with the ID of the first appointment
            var result = controller.Create(newAppointment, "dressname", "dresscolor");

            //Check the result, we should have gotten a RedirectToAction
            Assert.IsType<RedirectToActionResult>(result);

            //Check if the item was added to the database context
            Assert.Contains(newAppointment, dbContext.Appointment);
        }
    }
}
