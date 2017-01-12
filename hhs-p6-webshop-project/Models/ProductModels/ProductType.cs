using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ProductType
    {
        public ProductType() {
            PropertyValueCouplings = new List<PropertyValueCoupling>();
            Images = new List<ProductImage>();
        }

        [Key]
        public int ProductTypeId { get; set; }

        public string NameOverride { get; set; }

        public string DescriptionOverride { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public List<ProductImage> Images { get; set; }
        
        public ICollection<PropertyValueCoupling> PropertyValueCouplings { get; set; }

    }
}
