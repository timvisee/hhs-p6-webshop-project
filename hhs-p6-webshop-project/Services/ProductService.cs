using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.FilterModels;
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

        public ProductService(ApplicationDbContext dbContext)
        {
            DatabaseContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return DatabaseContext.Products
                .Include(p => p.ColorOptions)
                .ThenInclude(co => co.Images)
                .ToList();
        }

        private List<object>
            ParseFuckingAnoyingJsonArrayOfArraysToJustAFuckingListGodFuckingDamnitIHateThisFuckingBullshit(
                HashSet<object> objects)
        {
            List<object> values = new List<object>();

            foreach (object value in objects)
            {
                if (value is Int64)
                    values.Add(Convert.ToDouble(value));
                else
                    values.Add(value);
            }
            return values;
        }

        public List<ColorOption> GetColorOptions()
        {
            return DatabaseContext.ColorOptions.Distinct().ToList();
        }

        public Dictionary<string, HashSet<object>> GetFilters()
        {
            Dictionary<string, HashSet<object>> val = new Dictionary<string, HashSet<object>>();

            val.Add("Prijs", new HashSet<object>());

            val["Prijs"].Add(DatabaseContext.Products.Min(p => p.Price));
            val["Prijs"].Add(DatabaseContext.Products.Max(p => p.Price));

            val.Add("Kleur", new HashSet<object>());

            val["Kleur"].Add(DatabaseContext.ColorOptions.Select(co => co.Color).Distinct());

            return val;
        }

        public List<FilterBase> GetAllFilters()
        {
            List<FilterBase> filters = new List<FilterBase>();

            PriceFilter pf = new PriceFilter();
            pf.Min = DatabaseContext.Products.Min(p => p.Price);
            pf.Max = DatabaseContext.Products.Max(p => p.Price);

            ColorFilter cf = new ColorFilter();

            cf.Colors.AddRange(DatabaseContext.ColorOptions.Select(co => co.Color).Distinct());
            filters.Add(pf);
            filters.Add(cf);

            return filters;
        }

        public List<Product> Filter(List<FilterBase> filters)
        {
            List<Product> products = GetAllProducts();
            List<Product> results = new List<Product>();

            foreach (Product product in products)
            {
                if (product.IsMatch(filters))
                {
                    var temp = filters.Where(f => f.Name == "Kleur")
                        .Cast<ColorFilter>()
                        .SelectMany(cf => cf.Colors)
                        .Distinct()
                        .ToList();
                    product.Sort(temp);
                    results.Add(product);
                }
            }

            return results;
        }

        public List<FilterBase> ParseFilters(Dictionary<string, HashSet<object>> filters)
        {
            List<FilterBase> f = new List<FilterBase>();

            foreach (var pair in filters)
            {
                if (pair.Key == "Kleur")
                {
                    ColorFilter filt = new ColorFilter();
                    filt.Colors.AddRange(
                        ParseFuckingAnoyingJsonArrayOfArraysToJustAFuckingListGodFuckingDamnitIHateThisFuckingBullshit(
                                pair.Value)
                            .Cast<string>());

                    if (filt.Colors.Count > 0)
                        f.Add(filt);
                }

                if (pair.Key == "Prijs")
                {
                    var vals =
                        ParseFuckingAnoyingJsonArrayOfArraysToJustAFuckingListGodFuckingDamnitIHateThisFuckingBullshit(
                                pair.Value)
                            .Cast<double>();

                    PriceFilter filt = new PriceFilter();
                    filt.Min = vals.Min();
                    filt.Max = vals.Max();
                    f.Add(filt);
                }
            }

            return f;
        }

        public PagedResponse GetAllProductsPaged(int start, int count)
        {
            PagedResponse response = new PagedResponse();
            int prev = start - count;
            if (prev < 0)
                prev = 0;

            var products = GetAllProducts();
            if (start + count > products.Count)
            {
                int num = Math.Abs(products.Count - (start + count));
                response.Data = products.GetRange(start, num);
                response.PreviousPage = $"api/dressfinder/product/{prev}/{count}";
                response.NextPage = null;
            }
            else
            {
                response.Data = products.GetRange(start, count);
                response.PreviousPage = (prev == 0) ? null : $"api/dressfinder/product/{prev}/{count}";

                if (start + count >= products.Count)
                {
                    response.NextPage = null;
                }
                else
                {
                    response.NextPage = $"api/dressfinder/product/{start + count}/{count}";
                }
            }
            return response;
        }
    }
}