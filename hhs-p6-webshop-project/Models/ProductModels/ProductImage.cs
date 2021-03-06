﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ProductImage
    {
        public ProductImage()
        {
        }

        public ProductImage(string path)
        {
            Path = path;
        }

        [Key]
        public int ProductImageId { get; set; }

        [Required]
        [DisplayName("Afbeelding")]
        public string Path { get; set; }

        [DisplayName("Kleur")]
        public int ColorOptionId { get; set; }

        [DisplayName("Kleur")]
        public ColorOption ColorOption { get; set; }
    }
}