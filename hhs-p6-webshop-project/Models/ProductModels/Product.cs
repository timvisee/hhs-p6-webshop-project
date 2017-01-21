using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using hhs_p6_webshop_project.Models.FilterModels;

namespace hhs_p6_webshop_project.Models.ProductModels
{
    public class Product
    {
        public Product() {
            ColorOptions = new List<ColorOption>();
        }

        public int ProductId { get; set; }
        [DisplayName("Product")]
        public string Name { get; set; }

        [DisplayName("Beschrijving")]
        public string Description { get; set; }
        
        [DisplayName("Prijs")]
        public double Price { get; set; }

        [DisplayName("Kleuren")]
        public List<ColorOption> ColorOptions { get; set; }

        public Tuple<double, double, bool> ParsePriceRange(HashSet<object> set) {
            double min = (double) set.Min();
            double max = (double) set.Max();

            return new Tuple<double, double, bool>(min, max, min == max);
        }

        public void Sort(List<string> colors) {
            ColorOptions = ColorOptions.OrderByDescending(obj => obj.CompareTo(colors)).ToList();
            //ColorOptions.Sort((x, y) => x.CompareTo(colors) + y.CompareTo(colors));
        }

        public void Sort(string color) {
            Sort(new List<String> {
                color
            });
        }

        public bool IsMatch(List<FilterBase> filters) {
            bool isMatch = true;

            foreach (FilterBase filter in filters) {
                if (filter is ColorFilter) {
                    ColorFilter filt = filter as ColorFilter;
                    bool colorMatch = false;

                    foreach (ColorOption co in ColorOptions) {
                        if (filt.Colors.Contains(co.Color))
                            colorMatch = true;
                    }

                    if (!colorMatch)
                        isMatch = false;

                } else if (filter is PriceFilter) {
                    PriceFilter filt = filter as PriceFilter;
                    isMatch = Price >= filt.Min && Price <= filt.Max;
                }

                if (!isMatch) {
                    Console.WriteLine(
                        $"Filter mismatch for {this} on {filter.Name} with values ({string.Join(", ", filters)})!");
                    return false;
                }
                else {
                    Console.WriteLine($"Filter MATCH for {this} on {filter.Name} with values ({string.Join(", ", filters)})!");
                }
            }

            Console.WriteLine($"Filter MATCH on product {this}");
            return true;
        }

        public bool IsMatch(Dictionary<string, HashSet<object>> filterSet) {
            bool isMatch = true;

            foreach (KeyValuePair<string, HashSet<object>> filter in filterSet) {
                if (filter.Key == "Prijs") {
                    Tuple<double, double, bool> priceFilter = ParsePriceRange(filter.Value);

                    if (priceFilter.Item3) {
                        isMatch = Price == priceFilter.Item1;
                    }
                    else {
                        isMatch = Price >= priceFilter.Item1 && Price <= priceFilter.Item2;
                    }
                }

                if (filter.Key == "Kleur") {
                    bool colorMatch = false;

                    List<string> colorValues = new List<string>();

                    foreach (JArray value in filter.Value) {
                            for (int i = 0; i < value.Count; i++) {
                                colorValues.Add(value[i].Value<string>());
                            }
                        }

                    foreach (ColorOption co in ColorOptions) {
                        if (colorValues.Contains(co.Color))
                            colorMatch = true;
                    }

                    if (!colorMatch)
                        isMatch = false;
                }

                if (!isMatch) {
                    Console.WriteLine(
                        $"Filter mismatch for {this} on {filter.Key} with values ({string.Join(", ", filter.Value)})!");
                    return false;
                }
                else {
                    Console.WriteLine($"Filter MATCH for {this} on {filter.Key} with values ({string.Join(", ", filter.Value)})!");
                }
            }

            Console.WriteLine($"Filter MATCH on product {this}");
            return true;
        }

        public override string ToString() {
            return $"( {ProductId} -> '{Name}', {Price} )";
        }
    }
}
