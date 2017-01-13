using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hhs_p6_webshop_project.ProductDataModel
{
    public class Product
    {
        public Product() {
            ColorOptions = new List<ColorOption>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public List<ColorOption> ColorOptions { get; set; } 

    }
}