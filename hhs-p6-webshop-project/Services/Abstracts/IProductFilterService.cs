using System.Collections.Generic;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    public interface IProductFilterService
    {

        void Sort(Product product, List<string> colors);
        void Sort(Product product, string color);

        List<Product> Filter(List<Product> products, Dictionary<string, HashSet<object>> filters);
    }
}
