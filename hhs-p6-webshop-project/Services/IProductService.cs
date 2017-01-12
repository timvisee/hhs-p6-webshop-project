using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Models.ProductModels;

namespace hhs_p6_webshop_project.Services
{
    public interface IProductService {
        /// <summary>
        /// Returns a list of all products, sorted by Id, ascending.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all <see cref="Product"/> product instances.</returns>
        List<Product> GetAllProducts();

        List<PropertyValueCoupling> Test();

            /// <summary>
        /// Returns a list of all products, sorted by Id, ascending.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of all <see cref="Product"/> product instances.</returns>
        PagedResponse GetAllProductsPaged(int start, int count);

        /// <summary>
        /// Returns an <see cref="ArrayList"/> of all available <see cref="PropertyValue"/> for a given range of <see cref="Product"/>.
        /// </summary>
        /// <param name="products">the <see cref="Product"/> instances to return the available filters for</param>
        /// <returns>A <see cref="List{T}"/> of available filters for a given <see cref="Product"/> range</returns>
        List<PropertyValue> GetAllAvailableFiltersForProducts(List<Product> products);

        List<PropertyType> GetPropertyTypesForProduct(Product product);
        List<PropertyType> AllProductFilterTypesAsList();
    }
}
