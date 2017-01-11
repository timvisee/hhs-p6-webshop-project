using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class PropertyTypeProduct : IEquatable<PropertyTypeProduct> {

        public PropertyTypeProduct() {
            
        }

        public PropertyTypeProduct(Product product, PropertyType propertyType, PropertyValue propertyValue) {
            //Assign cross references
            Product = product;
            PropertyType = propertyType;
            PropertyValue = propertyValue;
        }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }

        public int PropertyValueId { get; set; }
        public PropertyValue PropertyValue { get; set; }

        public bool Equals(PropertyTypeProduct other) {
            return ProductId == other.ProductId && PropertyTypeId == other.PropertyTypeId && PropertyValueId == other.PropertyValueId;
        }
    }
}
