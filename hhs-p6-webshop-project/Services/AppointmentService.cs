using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Services
{
    public class AppointmentService : IAppointmentService
    {

        private ApplicationDbContext DatabaseContext { get; set; }

        public AppointmentService(ApplicationDbContext dbContext)
        {
            DatabaseContext = dbContext;
        }

        public List<Appointment> GetAllAppointments()
        {
            return DatabaseContext.Appointment.Include(a => a.AppointmentDateTime).ToList();
        }
    }
}