using System;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyType : IEquatable<PropertyType> {

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
        public override bool Equals(object other) {
            // Make sure the type is correct, and compare the actual property type if so
            return other.GetType() == typeof(PropertyType) && Equals((PropertyType) other);
        }

        /// <summary>
        /// Compare this property type to another property type.
        /// </summary>
        /// <param name="other">Other property type.</param>
        /// <returns>True if they are equal, false if not.</returns>
        public bool Equals(PropertyType other) {
            return ID == other.ID;
        }

    }
}
