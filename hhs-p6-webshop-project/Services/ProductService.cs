using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.ExtraModels;
using hhs_p6_webshop_project.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Tools.Compiler;
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
        }

        public List<ProductFilter> GetAllProductFilters() {
            List<ProductFilter> filters = new List<ProductFilter>();

            foreach (PropertyType pt in DatabaseContext.PropertyType) {
                var f = new ProductFilter();
                f.FilterType = pt;
                filters.Add(f);
            }

            var pvcs =
                DatabaseContext.PropertyValueCouplings
                .Include(pvc => pvc.PropertyType)
                .Include(pvc => pvc.PropertyValue)
                .Include(pvc => pvc.ProductType);

            foreach (PropertyValueCoupling pvc in pvcs) {
                var x = filters.Find(pf => pf.FilterType.Equals(pvc.PropertyType));

                bool add = true;

                foreach (PropertyValue v in x.Values) {
                    if (v.Value == pvc.PropertyValue.Value)
                        add = false;
                }

                if (add)
                    x.Values.Add(pvc.PropertyValue);
            }

            return filters;

        }

        public List<PropertyValueCoupling> Test() {
            return DatabaseContext.PropertyValueCouplings.ToList();
        }

        public List<ProductFilter> ParseFilterRequest(FilterRequest req) {

            //reconstruct filters
            var pvcs =
                DatabaseContext.PropertyValueCouplings
                .Include(pvc => pvc.PropertyType)
                .Include(pvc => pvc.PropertyValue)
                .Include(pvc => pvc.ProductType);

            var suppliedFilters = new List<ProductFilter>();

            foreach (PropertyValueCoupling pvc in pvcs) {
                if (req.Values.ContainsKey(pvc.PropertyTypeId)) {
                    if (req.Values[pvc.PropertyTypeId].Contains(pvc.PropertyValueId)) {
                        ProductFilter f = suppliedFilters.FirstOrDefault(pf => pf.FilterType.PropertyTypeId == pvc.PropertyTypeId);
                        if (f == null) {
                            f = new ProductFilter();
                            f.FilterType = pvc.PropertyType;
                            suppliedFilters.Add(f);
                        }
                        f.Values.Add(pvc.PropertyValue);
                    }
                }
            }
            

            foreach (var filter in suppliedFilters) {
                Console.WriteLine($"Selected filter: {filter.ToString()}");
            }

            return suppliedFilters;
        }

        public List<Product> Filter(List<ProductFilter> filters, List<Product> products) {
            List<Product> results = new List<Product>();

            foreach (Product p in products) {
                bool isMatch = false;
                foreach (ProductType pt in p.ProductTypes) {

                    //Get a list of all PropertyValues for the current ProductType
                    var pvcs =
                        DatabaseContext.PropertyValueCouplings
                        .Include(pvc => pvc.PropertyType)
                        .Include(pvc => pvc.PropertyValue)
                        .Include(pvc => pvc.ProductType)
                        .Where(pvc => pvc.ProductType == pt);

                    //Check if they match the filter
                    foreach (ProductFilter f in filters) {
                        var matchingFields = pvcs.Where(pvc => pvc.PropertyType.Equals(f.FilterType)).Select(pvc => pvc.PropertyValue);

                        //Now if atleast one filter value is present, the product matches
                        foreach (PropertyValue pv in f.Values) {
                            if (matchingFields.Any(pppp => pppp.Equals(pv)))
                                isMatch = true;
                        }
                    }

                    if (isMatch)
                        break;
                }
                if (isMatch)
                    results.Add(p);
            }

            return results;
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
