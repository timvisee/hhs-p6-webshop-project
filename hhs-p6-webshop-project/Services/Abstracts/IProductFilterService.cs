using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    public interface IProductFilterService
    {

        Tuple<double, double, bool> ParsePriceRange(HashSet<object> set);

        void Sort(Product product, List<string> colors);
        void Sort(Product product, string color);

        bool IsMatch(Product product, List<FilterBase> filters);
        bool IsMatch(Product product, Dictionary<string, HashSet<object>> filterSet);

    }
}
