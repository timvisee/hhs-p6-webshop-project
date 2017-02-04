using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Models.FilterModels;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Colors = new List<string>();
        }

        public List<Product> Products { get; set; }
       
        public List<FilterBase> Filters { get; set; }

        public double MaxPrice { get; set; }

        public double MinPrice { get; set; }

        public List<string> Colors { get; set; }
    }
}