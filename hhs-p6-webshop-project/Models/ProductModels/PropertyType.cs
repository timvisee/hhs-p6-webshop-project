using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyType {
        public int ID { get; set; }
        public String Name { get; set; }
        public String DataType { get; set; }
        public Boolean Required { get; set; }
        public Boolean Multiple { get; set; }
        public Boolean AllowCustom { get; set; }
    }
}
