using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace hhs_p6_webshop_project.Services
{
    /// <summary>
    /// Service to help with database querying for products and filtering the results.
    /// </summary>
    public class ProductService : IProductService
    {
        private ApplicationDbContext DatabaseContext { get; set; }
        private IProductFilterService FilterService { get; set; }

        public ProductService(ApplicationDbContext dbContext, IProductFilterService filterService)
        {
            DatabaseContext = dbContext;
            FilterService = filterService;
        }

        /// <summary>
        /// Returns a list of all products, sorted by Id, ascending.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all <see cref="Product"/> product instances.</returns>
        public List<Product> GetAllProducts()
        {
            return DatabaseContext.Products
                .Include(p => p.ColorOptions)
                .ThenInclude(co => co.Images)
                .ToList();
        }

        /// <summary>
        /// Builds a <see cref="ProductViewModel"/> based upon a <see cref="List{T}"/> of <see cref="Product"/>s and a <see cref="List{T}"/> of <see cref="FilterBase"/> objects.
        /// </summary>
        /// <param name="products">the products to populate this view with</param>
        /// <param name="filters">the filters to populate this view with</param>
        /// <returns></returns>
        public ProductViewModel BuildProductViewModel(List<Product> products, List<FilterBase> filters)
        {
            //Build the model and calculate the required properties
            var model = new ProductViewModel();
            model.Products = products;
            model.Filters = filters;

            if (filters != null && filters.Count > 0)
            {
                model.MaxPrice = filters.Where(f => f.Name == "Prijs").Cast<PriceFilter>().First().Max;
                model.MinPrice = filters.Where(f => f.Name == "Prijs").Cast<PriceFilter>().First().Min;
                model.Colors =
                    filters.Where(f => f.Name == "Kleur").Cast<ColorFilter>().SelectMany(cf => cf.Colors).ToList();
            }
            return model;
        }
        
        /// <summary>
        /// Returns a list of all available <see cref="ColorOption"/>s.
        /// </summary>
        /// <returns>a <see cref="List{T}"/> of <see cref="ColorOption"/></returns>
        public List<ColorOption> GetColorOptions()
        {
            return DatabaseContext.ColorOptions.Distinct().ToList();
        }

        /// <summary>
        /// Fetches a list of all available filter options as JSON serialization friendly object.
        /// </summary>
        /// <returns>a <see cref="Dictionary{TKey,TValue}"/> with a <see cref="string"/> as key and a <see cref="HashSet{T}"/> as value</returns>
        public Dictionary<string, HashSet<object>> GetFilters()
        {
            var val = new Dictionary<string, HashSet<object>>();

            val.Add("Prijs", new HashSet<object>());

            val["Prijs"].Add(DatabaseContext.Products.Min(p => p.Price));
            val["Prijs"].Add(DatabaseContext.Products.Max(p => p.Price));

            val.Add("Kleur", new HashSet<object>());

            val["Kleur"].Add(DatabaseContext.ColorOptions.Select(co => co.Color).Distinct());

            return val;
        }

        /// <summary>
        /// Detches a list of allavailable filter options as a C# friendly object.
        /// </summary>
        /// <returns>a <see cref="List{T}"/> of <see cref="FilterBase"/> objects</returns>
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

        /// <summary>
        /// Filters all available products based on a filter selection.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>a filtered <see cref="List{T}"/> of <see cref="Product"/> sorted by chosen filter options</returns>
        public List<Product> Filter(Dictionary<string, HashSet<object>> filters)
        {
            return FilterService.Filter(GetAllProducts(), filters);
        }
        
    }
}