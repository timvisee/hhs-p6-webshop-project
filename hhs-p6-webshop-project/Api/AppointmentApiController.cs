using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using hhs_p6_webshop_project.Data;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Controllers.Ajax
{
    [Produces("application/json")]
    [Route("api/appointments")]
    public class AppointmentApiController : Controller
    {
        /// <summary>
        /// Application database context.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public AppointmentApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetDates endpoint
        /// </summary>
        /// <returns>JSON response.</returns>
        [HttpGet("getdates")]
        public JsonResult GetDates()
        {
            return Json(_context.Appointment
                .GroupBy(a => a.AppointmentDateTime.Date)
                .Where(k => k.Count() >= 3)
                .Select(a => a.Key.ToString("yyyy-MM-dd")));
        }

        /// <summary>
        /// GetTimes endpoint
        /// </summary>
        /// <returns>JSON response.</returns>
        [HttpGet("gettimes")]
        public JsonResult GetTimes(string date)
        {
            // Determine the date to get the free times for
            DateTime dateObject = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var appointments = _context.Appointment.Where(a => a.AppointmentDateTime.Date == dateObject.Date)
                .Select(a => a.AppointmentDateTime)
                .ToList();

            var timeslots = new List<TimeSpan>();
            timeslots.Add(TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(0)));

            //var freeSlots = new List<TimeSpan>();

            List<Object> times = new List<Object>();

            foreach (var slot in timeslots)
            {
                bool taken = false;

                foreach (var app in appointments)
                {
                    if (app.TimeOfDay == slot)
                        taken = true;
                }


                times.Add(new
                {
                    available = !taken,
                    formattedTime = slot.ToString(@"hh\:mm"),
                    time = new
                    {
                        hour = slot.Hours,
                        minute = slot.Minutes,
                        second = slot.Seconds
                    }
                });
            }

            // Return a JSON result of available times
            return new JsonResult(new
            {
                times = times
            });
        }
    }
}