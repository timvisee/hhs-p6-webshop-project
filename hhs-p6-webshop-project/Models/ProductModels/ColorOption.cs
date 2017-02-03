using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ColorOption : IEquatable<ColorOption>, IComparable<List<string>>
    {
        public ColorOption()
        {
            Images = new List<ProductImage>();
        }

        [Key]
        [DisplayName("Kleur")]
        public int ColorOptionId { get; set; }

        [Required]
        [DisplayName("Kleur")]
        public string Color { get; set; }

        [DisplayName("Product")]
        public int ProductId { get; set; }

        [DisplayName("Product")]
        public Product Product { get; set; }

        [DisplayName("Afbeeldingen")]
        public List<ProductImage> Images { get; set; }

        public bool Equals(ColorOption other)
        {
            return Color == other.Color;
        }

        public int CompareTo(List<string> list)
        {
            if (list.Contains(Color))
                return 5;

            return -1;
        }
    }
}