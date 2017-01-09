using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyType : IEquatable<PropertyType> {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool Required { get; set; }
        public bool Multiple { get; set; }
        public bool AllowCustom { get; set; }
        public bool Equals(PropertyType other) {
            return ID == other.ID;
        }
    }
}
