using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Services {

    public class ProductFilterService : IProductService {

        private ApplicationDbContext DatabaseContext { get; set; }

        public ProductFilterService(ApplicationDbContext dbContext) {
            DatabaseContext = dbContext;
        }

        /// <summary>
        /// Get a list of products filtered by the given property values.
        /// </summary>
        /// <param name="values">Property values to filter on.</param>
        /// <returns>List of products. An empty list of products might be returned if the filters aren't consistent.</returns>
        public List<Product> GetProductsFiltered(List<PropertyValue> values) {
            // Get the queryable products
            IQueryable<Product> queryable = DatabaseContext.Product.AsQueryable();
            IQueryable<PropertyValue> queryableValue = DatabaseContext.PropertyValue.AsQueryable();

            // Get a list of property types that are used as filters
            HashSet<PropertyType> types = new HashSet<PropertyType>();
            values.ForEach(value => types.Add(value.PropertyType));

            // TODO: Unfinished! Complete this method.

            // Return the list of products
            return queryable.ToList();
        }

        public List<PropertyType> GetPropertyTypesForProduct(Product product) {
            return product.Properties.Select(type => type.PropertyType).Distinct().ToList();
        }

        // TODO: Can't we fetch all types from the ProductType model table here?
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
