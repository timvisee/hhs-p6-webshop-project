using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyValue : IEquatable<PropertyValue> {

        public PropertyValue() {
            
        }

        public PropertyValue(object value) {
            Value = value.ToString();
        }

        [Key]
        public int PropertyValueId { get; set; }

        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Compare this property value to another property value instance.
        /// </summary>
        /// <param name="other">Other property value instance to compare to.</param>
        /// <returns>True if the types are equal, false if not.</returns>
        public override bool Equals(object other) {
            // Make sure the type is correct, and compare the actual property value if so
            return other.GetType() == typeof(PropertyValue) && Equals((PropertyValue)other);
        }

        /// <summary>
        /// Compare this property value to another property value.
        /// </summary>
        /// <param name="other">Other property value.</param>
        /// <returns>True if they are equal, false if not.</returns>
        public bool Equals(PropertyValue other) {
            return PropertyValueId == other.PropertyValueId || Value.Equals(other.Value);
        }

    }
}
