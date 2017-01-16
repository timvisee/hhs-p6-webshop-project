using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ColorOption : IEquatable<ColorOption>, IComparable<List<string>> 
    {
        public ColorOption() {
            Images = new List<ProductImage>();
        }

        [Key]
        public int ColorOptionId { get; set; }

        [Required]
        public string Color { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public List<ProductImage> Images { get; set; }

        public bool Equals(ColorOption other) {
            return Color == other.Color;
        }

        public int CompareTo(List<string> list) {
            if (list.Contains(Color))
                return 5;

            return -1;
        }
    }
}
