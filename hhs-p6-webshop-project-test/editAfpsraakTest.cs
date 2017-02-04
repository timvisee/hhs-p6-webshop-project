using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Models.AppointmentModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace hhs_p6_webshop_project_test
{
    public class editAfpsraakTest
    {

       [Fact]
        public void Valid_Edit_AppointmentController()
        {
            //Instantieer de controller
            AppointmentsController c = new AppointmentsController();
            //Geef mee welke appointment er bewerkt moet worden
            var result = c.Edit(1);
            //Check of het goede wordt doorgelinkt

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }


    }
}
