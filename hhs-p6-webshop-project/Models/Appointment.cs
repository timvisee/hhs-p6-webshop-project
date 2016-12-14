using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models {
    public class Appointment {
        public int ID { get; set; }
        public string Mail { get; set; }
        public DateTime DateMarried { get; set; }
        public AppointmentTime AppointmentTime { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool Confirmation { get; set; }
    }
}
