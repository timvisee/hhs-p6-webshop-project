using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Api
{
    [Produces("application/json")]
    [Route("api/dressfinder")]
    public class DressFinderApiController : Controller
    {
        public IProductService ProductService { get; }
      
        public DressFinderApiController(IProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet("product/all")]
        public JsonResult GetAllProducts()
        {
            return Json(ProductService.GetAllProducts());
        }

        [HttpGet("product/filters")]
        public JsonResult GetAllFilters()
        {
            return Json(ProductService.GetFilters());
        }

        [HttpPost("product/filter/partial")]
        public PartialViewResult FilterPartial([FromBody] Dictionary<string, HashSet<object>> filters)
        {
            var products = ProductService.Filter(filters);
            
            var model = ProductService.BuildProductViewModel(products, null);

            return PartialView("~/Views/Products/ProductOverview.cshtml", model);
        }

        [HttpPost("product/filter/partial/sort/{id}")]
        public PartialViewResult FilterPartialSort([FromBody] Dictionary<string, HashSet<object>> filters, int id)
        {
            var products = ProductService.Filter(filters);

            switch (id)
            {
                case 0: //prijs laag->hoog
                    products = products.OrderBy(o => o.Price).ToList();
                    break;
                case 1: //prijs hoog->laag
                    products = products.OrderByDescending(o => o.Price).ToList();
                    break;
                case 2: //naam oplopend
                    products = products.OrderBy(o => o.Name).ToList();
                    break;
                case 3: //naam aflopend
                    products = products.OrderByDescending(o => o.Name).ToList();
                    break;
            }

            var model = ProductService.BuildProductViewModel(products, null);
            
            return PartialView("~/Views/Products/ProductOverview.cshtml", model);
        }

        [HttpPost("product/filter")]
        public JsonResult Filter([FromBody] Dictionary<string, HashSet<object>> filters)
        {
            return Json(ProductService.Filter(filters));
        }

        [HttpGet("product/test")]
        public JsonResult GetAll()
        {
            return Json(ProductService.GetColorOptions());
        }
        
    }
}