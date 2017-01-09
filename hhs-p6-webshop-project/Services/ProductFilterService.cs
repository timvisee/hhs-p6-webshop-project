using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.CodeAnalysis;

namespace hhs_p6_webshop_project.Services
{
    public class ProductFilterService : IProductService
    {

        private ApplicationDbContext DatabaseContext { get; set; }
        public ProductFilterService(ApplicationDbContext dbContext) {
            DatabaseContext = dbContext;
        }

        public List<PropertyType> GetPropertyTypesForProduct(Product product) {
            return product.Properties.Select(type => type.PropertyType).Distinct().ToList();
        } 

        public List<PropertyType> AllProductFilterTypesAsList() {
            HashSet<PropertyType> types = new HashSet<PropertyType>();

            foreach (var product in DatabaseContext.Product) {
                foreach (var value in product.Properties) {
                    types.Add(value.PropertyType);
                }
            }

            return types.ToList();
        }

    }
}
