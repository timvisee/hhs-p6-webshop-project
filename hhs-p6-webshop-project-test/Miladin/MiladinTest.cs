using hhs_p6_webshop_project.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.App.Config;
using Microsoft.Extensions.Options;
using hhs_p6_webshop_project_test.Roderick;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project.Services.Containers;

namespace hhs_p6_webshop_project_test.Miladin
{
    public class MiladinTest
    {
        private ApplicationDbContext _context;
        public static List<Appointment> FakeAppointmentList = new List<Appointment>() {
            new Appointment() {
                ID = 111,
                Mail = "n@s.nl",
                DateMarried = new DateTime(2019, 6, 10) ,
                AppointmentDateTime = new DateTime(2020, 6, 10),
                Name= "Beun1",
                Phone = "061212121",
                Confirmation = true


            },
             new Appointment() {
                ID = 222,
                Mail = "n@s.nl",
                DateMarried = new DateTime(2018, 6, 10) ,
                AppointmentDateTime = new DateTime(2020, 12, 10),
                Name= "Beun2",
                Phone = "061212121",
                Confirmation = false


            }
        };
        public static AppointmentsController SetMockAndGetAppointmentController()
        {
            var mockAppContext = Tools.MockTestDatabaseContext();

            var mockOptions = new Mock<IOptions<SecureAppConfig>>();
            var mockConfig = new Mock<SecureAppConfig>();
            var mockService = new Mock<ITransactionalEmailService>();
            mockService.Setup(s => s.SendAppointmentEmail(It.IsAny<AppointmentMessageContainer>()));
        
            mockConfig.Setup(c => c.Owner).Returns("Miladin");
            mockOptions.Setup(c => c.Value).Returns(mockConfig.Object);
            
            AppointmentsController a = new AppointmentsController(mockAppContext, mockOptions.Object, mockService.Object);

            return a;
        }
        
        [Fact]
        public void ErrorPageWhenInvalidID()
        {
            int id = -1;
            AppointmentsController a = SetMockAndGetAppointmentController();
            var result = a.Details(id);

            //result moet een not found zijn
            Assert.IsType<NotFoundResult>(result);
            
        }

        [Fact]
        public void CreateAppointmentTest()
        {
            string fakeDress = "fake";
            string fakecolor = "fake";

            AppointmentsController controller = SetMockAndGetAppointmentController();
            var mockAppContext = new Mock<ApplicationDbContext>();


            var Appointment = FakeAppointmentList.First();
            var result = controller.Create(Appointment, fakeDress, fakecolor);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ContactReturnViewTest()
        {
            //mock de parameters
            var cfgMock = new Mock<IOptions<SecureAppConfig>>();
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


            Assert.IsType<ViewResult>(result);
        }
    }
}