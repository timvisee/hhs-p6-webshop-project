using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyValue {
        public int ID { get; set; }
        public Product Product { get; set; }
        public PropertyType PropertyType { get; set; }
        public String Value { get; set; }

    }
}
