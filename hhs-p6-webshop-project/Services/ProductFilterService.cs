using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services.Abstracts;

namespace hhs_p6_webshop_project.Services
{
    /// <summary>
    /// Internal helper service to help with filtering the available products.
    /// </summary>
    internal class ProductFilterService : IProductFilterService
    {
        /// <summary>
        /// Filters the <see cref="ColorOption"/> list of a <see cref="Product"/> by the selected colors filters.
        /// </summary>
        /// <param name="product">the <see cref="Product"/> to sort</param>
        /// <param name="colors">the <see cref="List{T}"/> of selected colors</param>
        public void Sort(Product product, List<string> colors)
        {
            product.ColorOptions = product.ColorOptions.OrderByDescending(obj => obj.CompareTo(colors)).ToList();
        }

        /// <summary>
        /// Filters the <see cref="ColorOption"/> list of a <see cref="Product"/> by the selected color filter.
        /// </summary>
        /// <param name="product">the <see cref="Product"/> to sort</param>
        /// <param name="color">the <see cref="List{T}"/> of selected colors</param>
        public void Sort(Product product, string color)
        {
            Sort(product, new List<String>
            {
                color
            });
        }
        
        /// <summary>
        /// Filters all available products based on a filter selection.
        /// </summary>
        /// <param name="products">the <see cref="List{T}"/> of all available <see cref="Product"/></param>
        /// <param name="filters"></param>
        /// <returns>a filtered <see cref="List{T}"/> of <see cref="Product"/> sorted by chosen filter options</returns>
        public List<Product> Filter(List<Product> products, Dictionary<string, HashSet<object>> filters)
        {
            var filt = ParseFilters(filters);

            return Filter(products, filt.Item1, filt.Item2, filt.Item3);
        }

        #region Helper Methods

        private List<Product> Filter(List<Product> products, List<string> colors, double minPrice, double maxPrice)
        {
            var result = products;
            if (!maxPrice.Equals(0))
            {
                //We need to filter prices, only select products in the price range
                result = result.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            }

            if (colors.Count == 0)
                return products; //No color filters selected, no need to filter

            //Select products that match the selected colors
            result = result.Where(p => p.ColorOptions.Any(co => colors.Contains(co.Color))).ToList();
            //Sort all products pased on the selected colors (so that if the user filtered on blue, the blue dress images are shown first)
            result.ForEach(p => Sort(p, colors)); 

            return result;
        }

        private List<object> ParseJsonArray(HashSet<object> objects)
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

        private Tuple<List<string>, double, double>  ParseFilters(Dictionary<string, HashSet<object>> filters)
        {
            var colors = new List<string>();
            double min = 0;
            double max = 0;

            foreach (var pair in filters)
            {
                if (pair.Key == "Kleur")
                {
                    colors.AddRange(
                        ParseJsonArray(
                                pair.Value)
                            .Cast<string>());
                }

                if (pair.Key == "Prijs")
                {
                    var vals =
                        ParseJsonArray(
                                pair.Value)
                            .Cast<double>();

                    min = vals.Min();
                    max = vals.Max();
                }
            }

            return new Tuple<List<string>, double, double>(colors, min, max);
        }

        #endregion
    }
}
