using System.Collections.Generic;
using System.ComponentModel;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class Product
    {
        public Product()
        {
            ColorOptions = new List<ColorOption>();
        }

        public int ProductId { get; set; }

        [DisplayName("Product")]
        public string Name { get; set; }

        [DisplayName("Beschrijving")]
        public string Description { get; set; }

        [DisplayName("Prijs")]
        public double Price { get; set; }

        [DisplayName("Kleuren")]
        public List<ColorOption> ColorOptions { get; set; }

        public override string ToString()
        {
            return $"( {ProductId} -> '{Name}', {Price} )";
        }
    }
}