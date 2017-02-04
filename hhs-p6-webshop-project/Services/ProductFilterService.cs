using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services.Abstracts;
using Newtonsoft.Json.Linq;

namespace hhs_p6_webshop_project.Services
{
    public class ProductFilterService : IProductFilterService
    {
        public Tuple<double, double, bool> ParsePriceRange(HashSet<object> set)
        {
            double min = (double) set.Min();
            double max = (double) set.Max();

            return new Tuple<double, double, bool>(min, max, min == max);
        }

        public void Sort(Product product, List<string> colors)
        {
            product.ColorOptions = product.ColorOptions.OrderByDescending(obj => obj.CompareTo(colors)).ToList();
            //ColorOptions.Sort((x, y) => x.CompareTo(colors) + y.CompareTo(colors));
        }

        public void Sort(Product product, string color)
        {
            Sort(product, new List<String>
            {
                color
            });
        }

        public bool IsMatch(Product product, List<FilterBase> filters)
        {
            bool isMatch = true;

            foreach (FilterBase filter in filters)
            {
                if (filter is ColorFilter)
                {
                    ColorFilter filt = filter as ColorFilter;
                    bool colorMatch = false;

                    foreach (ColorOption co in product.ColorOptions)
                    {
                        if (filt.Colors.Contains(co.Color))
                            colorMatch = true;
                    }

                    if (!colorMatch)
                        isMatch = false;
                }
                else if (filter is PriceFilter)
                {
                    PriceFilter filt = filter as PriceFilter;
                    isMatch = product.Price >= filt.Min && product.Price <= filt.Max;
                }

                if (!isMatch)
                {
                    Console.WriteLine(
                        $"Filter mismatch for {this} on {filter.Name} with values ({string.Join(", ", filters)})!");
                    return false;
                }
                else
                {
                    Console.WriteLine(
                        $"Filter MATCH for {this} on {filter.Name} with values ({string.Join(", ", filters)})!");
                }
            }

            Console.WriteLine($"Filter MATCH on product {this}");
            return true;
        }

        public bool IsMatch(Product product, Dictionary<string, HashSet<object>> filterSet)
        {
            bool isMatch = true;

            foreach (KeyValuePair<string, HashSet<object>> filter in filterSet)
            {
                if (filter.Key == "Prijs")
                {
                    Tuple<double, double, bool> priceFilter = ParsePriceRange(filter.Value);

                    if (priceFilter.Item3)
                    {
                        isMatch = product.Price == priceFilter.Item1;
                    }
                    else
                    {
                        isMatch = product.Price >= priceFilter.Item1 && product.Price <= priceFilter.Item2;
                    }
                }

                if (filter.Key == "Kleur")
                {
                    bool colorMatch = false;

                    List<string> colorValues = new List<string>();

                    foreach (JArray value in filter.Value)
                    {
                        for (int i = 0; i < value.Count; i++)
                        {
                            colorValues.Add(value[i].Value<string>());
                        }
                    }

                    foreach (ColorOption co in product.ColorOptions)
                    {
                        if (colorValues.Contains(co.Color))
                            colorMatch = true;
                    }

                    if (!colorMatch)
                        isMatch = false;
                }

                if (!isMatch)
                {
                    Console.WriteLine(
                        $"Filter mismatch for {this} on {filter.Key} with values ({string.Join(", ", filter.Value)})!");
                    return false;
                }
                else
                {
                    Console.WriteLine(
                        $"Filter MATCH for {this} on {filter.Key} with values ({string.Join(", ", filter.Value)})!");
                }
            }

            Console.WriteLine($"Filter MATCH on product {this}");
            return true;
        }
    }
}
