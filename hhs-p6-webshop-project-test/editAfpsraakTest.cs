using hhs_p6_webshop_project.Controllers;
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
        public void Invalid_Edit_AfspraakController()
        {
            AppointmentsController a = new AppointmentsController();

            Student s = new Student() { Name = "Willem-Alexander" };

            var result = c.Edit(s);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(null, viewResult.ViewName);
            // als dezelfde naam als de action methode

            //Checking model enz.
            //Checking voor juiste foutboodschap bij de Model error
        }

    }
}
