using System.Collections.Generic;
using hhs_p6_webshop_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Api
{
    [Route("api/dressfinder")]
    public class DressFinderController : Controller
    {
        public IProductService ProductService { get; }

        public DressFinderController(IProductService productService) {
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
        public JsonResult FilterPartial([FromBody] Dictionary<string, HashSet<object>> filters) {
            // TODO: Used in previous implementation, should be upgraded to new models.
//            ProductViewModel pvm = new ProductViewModel();
//            if (request.Values.Count == 0)
//                pvm.Products = ProductService.GetAllProducts();
//            else
//                pvm.Products = ProductService.Filter(ProductService.ParseFilterRequest(request),
//                    ProductService.GetAllProducts());
//
//            pvm.Filters = ProductService.GetAllProductFilters();
//
//            return PartialView("~/Views/Products/ProductOverview.cshtml", pvm);

            // TODO: Temporary return here
            return Json(ProductService.Filter(ProductService.ParseFilters(filters)));
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

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
