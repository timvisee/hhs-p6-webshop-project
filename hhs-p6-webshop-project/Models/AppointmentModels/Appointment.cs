using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.AppointmentModels {
    public class Appointment {
        public int ID { get; set; }

        [DisplayName("E-Mail")]
        public string Mail { get; set; }

        [DisplayName("Trouwdatum")]
        public DateTime DateMarried { get; set; }

        [DisplayName("Datum afspraak")]
        public AppointmentTime AppointmentTime { get; set; }

        [DisplayName("Voor- en achternaam")]
        public string Name { get; set; }

        [DisplayName("Telefoonnummer")]
        public string Phone { get; set; }

        [DisplayName("Bevestigd")]
        public bool Confirmation { get; set; }
    }
}
