using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels {
    public class Product : IEquatable<Product> {

        public Product() {
            ProductTypes = new List<ProductType>();
        }

        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductType> ProductTypes { get; set; }

        /// <summary>
        /// Compare this product to another product instance.
        /// </summary>
        /// <param name="other">Other product instance to compare to.</param>
        /// <returns>True if the types are equal, false if not.</returns>
        public override bool Equals(object other) {
            // Make sure the type is correct, and compare the actual product if so
            return other.GetType() == typeof(Product) && Equals((Product)other);
        }

        /// <summary>
        /// Compare this product to another product.
        /// </summary>
        /// <param name="other">Other product.</param>
        /// <returns>True if they are equal, false if not.</returns>
        public bool Equals(Product other) {
            return ProductId == other.ProductId;
        }
    }
}
