using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.AppointmentModels {
    public class AppointmentTime {
        public int ID { get; set; }

        [DisplayName("Datum afspraak")]
        public DateTime DateTime { get; set; }
    }
}
