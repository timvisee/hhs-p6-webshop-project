using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models;
using hhs_p6_webshop_project.Services.Containers;
using SparkPost;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    /// <summary>
    /// Container interface for the Transactional Email System.
    /// </summary>
    public interface ITransactionalEmailService
    {
        Task<SendTransmissionResponse> SendContactEmail(ContactModels container);

        Task<SendTransmissionResponse> SendAppointmentEmail(AppointmentMessageContainer container);
    }
}
