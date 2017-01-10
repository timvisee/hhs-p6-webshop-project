using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyType {

        public int ID { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool Required { get; set; }
        public bool Multiple { get; set; }
        public bool AllowCustom { get; set; }

        /// <summary>
        /// Compare this property type to another property type instance.
        /// </summary>
        /// <param name="other">Other property type instance to compare to.</param>
        /// <returns>True if the types are equal, false if not.</returns>
        public bool Equals(object other) {
            // Return false if the object isn't a PropertyType isntance
            if(other.GetType() != typeof(PropertyType))
                return false;

            // Get the other property type
            PropertyType otherType = (PropertyType) other;

            // Compare the IDs and return the result
            return ID == otherType.ID;
        }

    }
}
