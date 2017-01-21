using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hhs_p6_webshop_project.Api
{
    [Route("api/dressfinder")]
    public class ProductsController : Controller
    {
        public IProductService ProductService { get; }

        public ProductsController(IProductService productService) {
            ProductService = productService;
        }

        [HttpGet("product/all")]
        public JsonResult GetAllProducts() {
            return Json(ProductService.GetAllProducts());
        }

        [HttpGet("product/filters")]
        public JsonResult GetAllFilters() {
            return Json(ProductService.GetFilters());
        }

        [HttpPost("product/filter/partial")]
        public PartialViewResult FilterPartial([FromBody] Dictionary<string, HashSet<object>> filters) {
            
            ProductView view = new ProductView();
            view.Products = ProductService.Filter(ProductService.ParseFilters(filters));

            return PartialView("~/Views/Products/ProductOverview.cshtml", view);

        }

        [HttpPost("product/filter/partial/sort/{id}")]
        public PartialViewResult FilterPartialSort([FromBody] Dictionary<string, HashSet<object>> filters, int id) {
            var p = ProductService.Filter(ProductService.ParseFilters(filters));

            switch (id) {
                case 0: //prijs laag->hoog
                    p = p.OrderBy(o => o.Price).ToList();
                    break; 
                case 1: //prijs hoog->laag
                    p = p.OrderByDescending(o => o.Price).ToList();
                    break;
                case 2: //naam oplopend
                    p = p.OrderBy(o => o.Name).ToList();
                    break;
                case 3: //naam aflopend
                    p = p.OrderByDescending(o => o.Name).ToList();
                    break;
            }
            
            ProductView view = new ProductView();
            view.Products = p;

            return PartialView("~/Views/Products/ProductOverview.cshtml", view);
        }

        [HttpPost("product/filter")]
        public JsonResult Filter([FromBody] Dictionary<string, HashSet<object>> filters) {
            return Json(ProductService.Filter(ProductService.ParseFilters(filters)));
        }

        [HttpGet("product/test")]
        public JsonResult GetAll() {
            return Json(ProductService.GetColorOptions());
        }

        [HttpGet("product/{start}/{count}")]
        public JsonResult GetAllProductsPaged(int start, int count) {
            return Json(ProductService.GetAllProductsPaged(start, count));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
