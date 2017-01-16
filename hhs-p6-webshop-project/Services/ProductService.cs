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
using Newtonsoft.Json.Linq;

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

        private List<object>
            ParseFuckingAnoyingJsonArrayOfArraysToJustAFuckingListGodFuckingDamnitIHateThisFuckingBullshit(HashSet<object> objects) {
            List<object> values  = new List<object>();

            foreach (JArray value in objects) {
                for (int i = 0; i < value.Count; i++) {
                    values.Add(value[i].Value<string>());
                }
            }
            return values;
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
            List<Product> results = new List<Product>();

            Dictionary<string, List<object>> betterList = new Dictionary<string, List<object>>();

            foreach (KeyValuePair<string, HashSet<object>> pair in filterSet) {
                if (pair.Key == "Kleur")
                    betterList.Add(pair.Key, ParseFuckingAnoyingJsonArrayOfArraysToJustAFuckingListGodFuckingDamnitIHateThisFuckingBullshit(pair.Value));
            }

            foreach (Product product in products) {
                if (product.IsMatch(filterSet)) {

                    var temp = betterList.Where(kv => kv.Key == "Kleur").Select(kv => kv.Value).SelectMany(x => x).Cast<string>().ToList();
                    product.Sort(temp);
                    results.Add(product);
                }
            }
            
            return results;
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
