using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyValue : IEquatable<PropertyType> {
        public int ID { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Value { get; set; }

        public bool Equals(PropertyType other) {
            return (ID == other.ID);
        }
    }
}
