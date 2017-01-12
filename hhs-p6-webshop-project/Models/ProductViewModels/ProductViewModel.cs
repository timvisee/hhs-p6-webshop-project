using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.ExtraModels;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } 
        public List<ProductFilter> Filters { get; set; } 
    }
}
