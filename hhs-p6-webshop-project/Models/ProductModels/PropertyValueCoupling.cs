using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyValueCoupling : IEquatable<PropertyValueCoupling> {

        public PropertyValueCoupling() {
            
        }

        public PropertyValueCoupling(ProductType productType, PropertyType propertyType, PropertyValue propertyValue) {
            ProductType = productType;
            PropertyType = propertyType;
            PropertyValue = propertyValue;
        }

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public int PropertyValueId { get; set; }
        public PropertyValue PropertyValue { get; set; }

        public bool Equals(PropertyValueCoupling other) {
            return ProductTypeId == other.ProductTypeId && PropertyTypeId == other.PropertyTypeId && PropertyValueId == other.PropertyValueId;
        }

        public override string ToString() {
            return $"(ProductTypeId: {ProductTypeId}, PropertyTypeId: {PropertyTypeId}, PropertyValueId: {PropertyValueId} | {ProductType.NameOverride}, {PropertyType.Name}, {PropertyValue.Value})";
        }
    }
}
