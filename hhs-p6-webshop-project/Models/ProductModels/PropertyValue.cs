using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyValue {

        public int ID { get; set; }
        public PropertyType PropertyType { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// Compare this property value to an other property value instance.
        /// </summary>
        /// <param name="other">Other property value instance.</param>
        /// <returns>True if the property values are equal (value-wise).</returns>
        public override bool Equals(object other) {
            // Return false if the other object isn't a property value instance
            if(other.GetType() != typeof(PropertyValue))
                return false;

            // Get the property value instance
            PropertyValue otherValue = (PropertyValue) other;

            // Return true if the IDs equal
            if(ID == otherValue.ID)
                return true;

            // Compare the value's and return the result
            return Value.Equals(otherValue.Value);
        }

    }
}
