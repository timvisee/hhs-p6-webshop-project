using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using hhs_p6_webshop_project.Data;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Api
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
            DateTime selectedDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var timeslots = new List<TimeSpan>();
            timeslots.Add(TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(30)));
            timeslots.Add(TimeSpan.FromHours(15).Add(TimeSpan.FromMinutes(0)));

            return Json(timeslots.Select(t => new
            {
                available = !_context.Appointment.Any(a => a.AppointmentDateTime == selectedDate.Add(t)),
                formattedTime = t.ToString(@"hh\:mm"),
                time = new
                {
                    hour = t.Hours,
                    minute = t.Minutes,
                    second = t.Seconds
                }
            }));
        }
    }
}