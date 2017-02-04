using System;
using System.Linq;
using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Controllers.Ajax;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace hhs_p6_webshop_project_test {

    [Collection("main")]
    public class AppointmentTests {

        /// <summary>
        /// Database fixture instance, used for testing.
        /// </summary>
        private DatabaseFixture _databaseFixture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseFixture">Database fixture.</param>
        public AppointmentTests(DatabaseFixture databaseFixture) {
            this._databaseFixture = databaseFixture;
        }

        [Fact]
        public void AppointmentTest() {
            Console.WriteLine("######### GETTING DATA FROM DB: ");
            Console.WriteLine(this._databaseFixture.Context.Appointment.ToList());
        }
    }
}
