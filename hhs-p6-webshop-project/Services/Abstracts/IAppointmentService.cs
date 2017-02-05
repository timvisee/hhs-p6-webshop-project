using System.Collections.Generic;
using hhs_p6_webshop_project.Models.AppointmentModels;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    public interface IAppointmentService
    {
        List<Appointment> GetAllAppointments();
    }
}