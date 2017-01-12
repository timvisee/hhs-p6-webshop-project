using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Services
{
    public class ProductService : IProductService
    {

        private ApplicationDbContext DatabaseContext { get; set; }
        public ProductService(ApplicationDbContext dbContext) {
            DatabaseContext = dbContext;
        }

        public List<Product> GetAllProducts() {
            return
                DatabaseContext.Product
                    .Include(p => p.ProductTypes)
                        .ThenInclude(pt => pt.Images)
                    .Include(p => p.ProductTypes)
                        .ThenInclude(pt => pt.PropertyValueCouplings)
                            .ThenInclude(pvc => pvc.ProductType)
                    .Include(p => p.ProductTypes)
                        .ThenInclude(pt => pt.PropertyValueCouplings)
                            .ThenInclude(pvc => pvc.PropertyType)
                    .Include(p => p.ProductTypes)
                        .ThenInclude(pt => pt.PropertyValueCouplings)
                            .ThenInclude(pvc => pvc.PropertyValue)
                    .ToList();

            //return DatabaseContext.Product
            //    .Include(p => p.PropertyTypeProducts)
            //    .ThenInclude(ptp => ptp.PropertyType)
            //    .Include(p => p.PropertyTypeProducts)
            //    .ThenInclude(ptp => ptp.PropertyValue)
            //    .ToList();
        }

        public List<PropertyValueCoupling> Test() {
            return DatabaseContext.PropertyValueCouplings.ToList();
        }

        /// <summary>
        /// Get a list of products filtered by the given property values.
        /// </summary>
        /// <param name="values">Property values to filter on.</param>
        /// <returns>List of products. An empty list of products might be returned if the filters aren't consistent.</returns>
        public List<Product> GetProductsFiltered(List<PropertyValue> values) {
            // Get the queryable products
//            IQueryable<Product> queryable = DatabaseContext.Product.AsQueryable();
//            IQueryable<PropertyValue> queryableValue = DatabaseContext.PropertyValue.AsQueryable();

            // Get a list of property types that are used as filters
            HashSet<PropertyType> types = new HashSet<PropertyType>();
          //  values.ForEach(value => types.Add(value.PropertyType));

            // TODO: Unfinished! Complete this method.

            // Return the list of products
//            return queryable.ToList();
            return null;
        }

        public PagedResponse GetAllProductsPaged(int start, int count) {
            PagedResponse response = new PagedResponse();
            int prev = start - count;
            if (prev < 0)
                prev = 0;

            var products = GetAllProducts();
            if (start + count > products.Count) {
                int num = Math.Abs(products.Count - (start + count));
                response.Data = products.GetRange(start, num);
                response.PreviousPage = $"api/dressfinder/product/{prev}/{count}";
                response.NextPage = null;
            }
            else {
                response.Data = products.GetRange(start, count);
                response.PreviousPage = (prev == 0) ? null : $"api/dressfinder/product/{prev}/{count}";

                if (start + count >= products.Count) {
                    response.NextPage = null;
                }
                else {
                    response.NextPage = $"api/dressfinder/product/{start + count}/{count}";
                }
                
            }
            return response;
        }

        public List<PropertyValue> GetAllAvailableFiltersForProducts(List<Product> products) {
            return null;
//            return products.Select(product => product.Properties).Distinct().ToList().SelectMany(item => item).ToList();
        }

        public List<PropertyType> GetPropertyTypesForProduct(Product product) {
//            return product.Properties.Select(type => type.PropertyType).Distinct().ToList();
            return null;
        } 

        public List<PropertyType> AllProductFilterTypesAsList() {
            HashSet<PropertyType> types = new HashSet<PropertyType>();

//            foreach (var product in DatabaseContext.Product) {
//                foreach (var value in product.Properties) {
//                    types.Add(value.PropertyType);
//                }
//            }

//            return types.ToList();
            return null;
        }

    }
}
