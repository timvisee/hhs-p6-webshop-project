using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        public JsonResult GetTimes(string date) {
            // Determine the date to get the free times for
            DateTime dateObject = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var appointments = _context.Appointment.Where(a => a.AppointmentDateTime.Date == dateObject.Date)
                .Select(a => a.AppointmentDateTime).ToList();

            var timeslots = new List<TimeSpan>();
            timeslots.Add(TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(0)));

            //var freeSlots = new List<TimeSpan>();

            List<Object> times = new List<Object>();

            foreach (var slot in timeslots) {

                bool taken = false;

                foreach(var app in appointments) {
                    if (app.TimeOfDay == slot)
                        taken = true;

                }


                times.Add(new {
                    available = !taken,
                    formattedTime = slot.ToString("hh:mm"),
                    time = new {
                        hour = slot.Hours,
                        minute = slot.Minutes,
                        second = slot.Seconds
                    }
                });


            }

            // Return the list of times
            return new AjaxResponse().SetDataField("times", times);
        }
    }
}