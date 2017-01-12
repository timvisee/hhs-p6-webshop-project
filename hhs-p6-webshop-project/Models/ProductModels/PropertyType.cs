using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hhs_p6_webshop_project.App;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyType : IEquatable<PropertyType> {

        [Key]
        public int PropertyTypeId { get; set; }

        [Required, DisplayName("Eigenschap naam")]
        public string Name { get; set; }

        [Required, DisplayName("Eigenschap type")]
        public string DataType { get; set; }

        [DisplayName("Vereist")]
        public bool Required { get; set; }

        [DisplayName("Sta meerdere waarden toe")]
        public bool Multiple { get; set; }

        [DisplayName("Sta custom waarde toe")]
        public bool AllowCustom { get; set; }

        /// <summary>
        /// Get the property data type by the current data type.
        /// </summary>
        /// <returns></returns>
        public PropertyDataType GetPropertyDataType() {
            return PropertyDataType.GetByDataType(this.DataType);
        }

        /// <summary>
        /// Set the property data type.
        /// </summary>
        /// <param name="dataType">Property data type.</param>
        public void SetPropertyDataType(PropertyDataType dataType) {
            this.DataType = dataType.dataType;
        }

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
            return PropertyTypeId == other.PropertyTypeId;
        }

        public override string ToString() {
            return Name;
        }
    }
}
