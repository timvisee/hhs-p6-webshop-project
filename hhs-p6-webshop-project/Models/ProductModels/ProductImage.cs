using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels
{
        public class ProductImage {

        public ProductImage() {
            
        }

        public ProductImage(string path) {
            Path = path;
        }

        [Key]
        public int ProductImageId { get; set; }

        [Required]
        public string Path { get; set; }

        public int ColorOptionId { get; set; }
        public ColorOption ColorOption { get; set; }
    }
}
