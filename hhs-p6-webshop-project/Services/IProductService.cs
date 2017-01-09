using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Services
{
    public interface IProductService {
        List<PropertyType> GetPropertyTypesForProduct(Product product);
        List<PropertyType> AllProductFilterTypesAsList();
    }
}
