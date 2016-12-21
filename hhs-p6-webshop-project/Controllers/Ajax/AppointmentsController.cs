using System;
using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.App.Ajax;
using hhs_p6_webshop_project.Data;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Controllers.Ajax {

    [Route("Ajax/Appointments")]
    public class AppointmentController : Controller {

        /// <summary>
        /// Application database context.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public AppointmentController(ApplicationDbContext context) {
            _context = context;
        }

        /// <summary>
        /// Endpoint index.
        /// </summary>
        /// <returns>JSON response.</returns>
        [HttpGet("")]
        public JsonResult Index() {
            // Respond with a not found error
            return new AjaxResponse(new NotFoundErrorStatus());
        }

        /// <summary>
        /// GetDates endpoint
        /// </summary>
        /// <returns>JSON response.</returns>
        [HttpGet("GetDates")]
        public JsonResult GetDates() {
            // Determine the base date time, get the appointments after it
            DateTime afterDateTime = DateTime.Now;

            // TODO: Fetch the actual occupied dates from the database!
//            // Fetch the occupied dates from the database
//            var dates = _context.Appointment
//                .Where(appointment => appointment.AppointmentDateTime > afterDateTime)
//                .SelectMany(appointment => appointment.AppointmentDateTime);

            // Create a list of dummy dates
            List<string> occupiedDates = new List<string> {
                DateTime.Today.ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"),
                DateTime.Now.AddDays(5).ToString("yyyy-MM-dd")
            };

            // Return the data fields
            return new AjaxResponse().SetDataField("dates", occupiedDates);
        }

        /// <summary>
        /// GetTimes endpoint
        /// </summary>
        /// <returns>JSON response.</returns>
        [HttpGet("GetTimes")]
        public JsonResult GetTimes() {
            // Determine the date to get the free times for
            DateTime date = DateTime.Now;

            // TODO: Fetch the free times from the database!
//            // Fetch the occupied dates from the database
//            var dates = _context.Appointment
//                .Where(appointment => appointment.AppointmentDateTime > afterDateTime)
//                .SelectMany(appointment => appointment.AppointmentDateTime);

            // Create a list of dummy dates
            List<Object> times = new List<Object> {
                new {
                    available = true,
                    time = "13:00:00"
                },
                new {
                    available = false,
                    time = "15:00:00"
                },
                new {
                    available = true,
                    time = "17:00:00"
                },
            };

            // Return the list of times
            return new AjaxResponse().SetDataField("times", times);
        }
    }
}