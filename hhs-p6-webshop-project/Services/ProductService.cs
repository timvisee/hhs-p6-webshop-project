using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace hhs_p6_webshop_project.Services
{
    public class ProductService : IProductService
    {

        private ApplicationDbContext DatabaseContext { get; set; }
        public ProductService(ApplicationDbContext dbContext) {
            DatabaseContext = dbContext;
        }

        public List<Product> GetAllProducts() {
            return DatabaseContext.Product.ToList();
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
            return products.Select(product => product.Properties).Distinct().ToList().SelectMany(item => item).ToList();
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
