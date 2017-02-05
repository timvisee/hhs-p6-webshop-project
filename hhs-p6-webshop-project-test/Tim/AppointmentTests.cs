using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace hhs_p6_webshop_project_test.Tim {

    [Collection("main")]
    public class AppointmentTests {

        /// <summary>
        /// A date that is fully booked.
        /// </summary>
        private static readonly String DATE_FULL = "2017-01-26";

        /// <summary>
        /// A date that is partially booked.
        /// </summary>
        private static readonly String DATE_PARTIAL = "2017-01-24";

        /// <summary>
        /// A date that isn't booked at all.
        /// </summary>
        private static readonly String DATE_EMPTY = "2017-01-27";

        /// <summary>
        /// Constant list of dates used for testing purposes.
        /// </summary>
        static readonly List<DateTime> TestDates = new List<DateTime>()
        {
            DateTime.Parse(DATE_PARTIAL + " 09:00:00"),
            DateTime.Parse("2017-01-25 12:30:00"),
            DateTime.Parse(DATE_FULL + " 09:00:00"),
            DateTime.Parse(DATE_FULL + " 12:30:00"),
            DateTime.Parse(DATE_FULL + " 15:00:00"),
        };

        /// <summary>
        /// Create a constant list of appointments based on the template list.
        /// </summary>
        /// <returns>Constant list of appointments.</returns>
        private List<Appointment> CreateTestAppointments()
        {
            // Stream the list of testing dates to appointments
            return TestDates.Select(dateTime => new Appointment()
            {
                AppointmentDateTime = dateTime
            }).ToList();
        }

        /// <summary>
        /// Test whether the response given by the API to get the unavailable dates works.
        /// </summary>
        [Fact]
        public void GetDatesTest()
        {
            // Set up the mock for the service
            var mockAppointmentService = new Mock<IAppointmentService>();
            mockAppointmentService.Setup(a => a.GetAllAppointments()).Returns(CreateTestAppointments);

            // Get the mocked appointment API controller
            var appointmentApiController = new AppointmentApiController(mockAppointmentService.Object);

            // Get the unavailable dates
            var datesResponse = appointmentApiController.GetDates();

            // Make sure the response is of the correct type
            Assert.IsType<JsonResult>(datesResponse);

            // Get the list of dates as dynamic object
            var dates = datesResponse.Value as dynamic;

            // Loop through the values, keep track of the count
            int count = 0;
            foreach (var dateString in dates)
            {
                // Make sure the value is a string
                Assert.IsType<String>(dateString);

                // Make sure this date equals the full date
                Assert.Equal(dateString, DATE_FULL);

                // Increase the count
                count++;
            }

            // Assert the count
            Assert.Equal(count, 1);
        }

        /// <summary>
        /// Test whether the response given by the API to get the unavailable dates works.
        /// </summary>
        [Fact]
        public void GetTimesTest()
        {
            // Set up the mock for the service
            var mockAppointmentService = new Mock<IAppointmentService>();
            mockAppointmentService.Setup(a => a.GetAllAppointments()).Returns(CreateTestAppointments);

            // Get the mocked appointment API controller
            var appointmentApiController = new AppointmentApiController(mockAppointmentService.Object);

            // Get the unavailable dates
            var timesResponse = appointmentApiController.GetTimes(DATE_FULL);

            // Make sure the response is of the correct type
            Assert.IsType<JsonResult>(timesResponse);

            // Get the list of times as dynamic object
            var times = timesResponse.Value as dynamic;

            // Loop through the values, keep track of the count
            int timeCount = 0;
            foreach (var timeObject in times)
            {
                // Make sure the time is occupied
                Assert.False(dynamicTimeObject.available);

                // Increase the time count
                timeCount++;
            }

            // Assert the count
            Assert.Equal(timeCount, 3);
        }
    }
}
