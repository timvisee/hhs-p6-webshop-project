using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.ProductDataModel
{
    public class ColorOption
    {
        public ColorOption() {
            Images = new List<ProductImage>();
        }

        [Key]
        public int ColorOptionId { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public ProductImage MainImage { get; set; }

        public List<ProductImage> Images { get; set; } 

    }
}
