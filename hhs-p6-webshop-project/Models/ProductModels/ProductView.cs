

using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Models.FilterModel;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class ProductView
    {
        public List<Product> Products { get; set; }
        //        public List<ProductFilter> Filters { get; set; }

        public List<FilterBase> Filters { get; set; }

        public double MaxPrice {
            get {
                return Filters.Where(f => f.Name == "Prijs").Cast<PriceFilter>().First().Max;
            }
        }

        public double MinPrice {
            get {
                return Filters.Where(f => f.Name == "Prijs").Cast<PriceFilter>().First().Min;
            }
        }

        public List<string> Colors {
            get {
                return Filters.Where(f => f.Name == "Kleur").Cast<ColorFilter>().SelectMany(cf => cf.Colors).ToList();
            }
        }
    }
}
