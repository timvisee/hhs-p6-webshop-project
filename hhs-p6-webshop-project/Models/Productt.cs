using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models {
    public class Productt {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public Decimal? UnitPrice { get; set; }
    }
}
