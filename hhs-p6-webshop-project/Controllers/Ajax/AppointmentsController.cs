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
            // Fetch the occupied dates from the database
            var dates = _context.Appointment.Where(a => a.AppointmentDateTime > afterDateTime).ToList();

            // Create a list to put the occupied dates in
            HashSet<string> occupiedDates = new HashSet<string>();

            //            // TODO: We should check whether the date is fully occupied!
            //            // Fill the list with the occupied dates

            foreach (var appointment in dates) {
                var amount = 0;

                foreach (var appointment2 in dates) {
                    if (appointment.AppointmentDateTime.Date == appointment2.AppointmentDateTime.Date) {
                        amount++;
                    }

                    if (amount >= 3) {
                        occupiedDates.Add(appointment.AppointmentDateTime.ToString("yyyy-MM-dd"));
                        break;
                    }
                }
            }

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
            //                .Where(appointment => appointment.AppointmentDateTime > date)
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