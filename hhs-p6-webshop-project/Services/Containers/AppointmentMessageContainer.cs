using System;

namespace hhs_p6_webshop_project.Services.Containers
{
    public class AppointmentMessageContainer
    {
        public string Name { get; set; }
        public string Recipient { get; set; }
        public DateTime Date { get; set; }
        public string Garment { get; set; }
    }
}
