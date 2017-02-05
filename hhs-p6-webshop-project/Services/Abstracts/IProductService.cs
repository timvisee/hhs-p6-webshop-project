using System.Collections.Generic;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Services.Abstracts
{
    public interface IProductService
    {
        
        List<Product> GetAllProducts();

        List<ColorOption> GetColorOptions();

        Dictionary<string, HashSet<object>> GetFilters();

        List<FilterBase> GetAllFilters();

        ProductViewModel BuildProductViewModel(List<Product> products, List<FilterBase> filters);

        List<Product> Filter(Dictionary<string, HashSet<object>> filters);
    }
}