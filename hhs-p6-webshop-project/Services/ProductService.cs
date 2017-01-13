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
            return DatabaseContext.Products
                .Include(p => p.ColorOptions)
                .ThenInclude(co => co.Images)
                .ToList();
            
        }

        public List<ColorOption> GetColorOptions() {
            return DatabaseContext.ColorOptions.Distinct().ToList();
        }

        public Dictionary<string, HashSet<object>> GetFilters() {
            Dictionary<string, HashSet<object>> val = new Dictionary<string, HashSet<object>>();

            val.Add("Prijs", new HashSet<object>());

            val["Prijs"].Add(DatabaseContext.Products.Min(p => p.Price));
            val["Prijs"].Add(DatabaseContext.Products.Max(p => p.Price));

            val.Add("Kleur", new HashSet<object>());

            val["Kleur"].Add(DatabaseContext.ColorOptions.Select(co => co.Color).Distinct());

            return val;
        }

        public List<Product> Filter(Dictionary<string, HashSet<object>> filterSet)
        {
            List<Product> products = GetAllProducts();


            return null;
            //foreach ()

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

    }
}
