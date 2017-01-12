using System.Collections.Generic;
using hhs_p6_webshop_project.ExtraModels;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Models.ProductViewModels;
using hhs_p6_webshop_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace hhs_p6_webshop_project.Api
{
    [Route("api/dressfinder")]
    public class DressFinderController : Controller
    {
        public IProductService ProductService { get;set; }

        public DressFinderController(IProductService productService) {
            ProductService = productService;
        }

        [HttpPost("product/filter/partial")]
        public PartialViewResult FilterPartial([FromBody] FilterRequest request) {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Products = ProductService.Filter(ProductService.ParseFilterRequest(request),
                ProductService.GetAllProducts());

            pvm.Filters = ProductService.GetAllProductFilters();

            return PartialView("~/Views/Products/ProductOverview.cshtml", pvm);
        }

        [HttpGet("product/all")]
        public JsonResult GetAllProducts() {
            return Json(ProductService.GetAllProducts());
        }

        // POST api/1/image/public/upload
        [HttpPost("product/filter")]
        public JsonResult Upload([FromBody] FilterRequest req) {
            if (req == null || req.Values.Count == 0)
                return Json(ProductService.GetAllProducts());

            return Json(ProductService.Filter(ProductService.ParseFilterRequest(req), ProductService.GetAllProducts()));
        }

        [HttpGet("product/filters")]
        public JsonResult GetAll() {
            return Json(ProductService.GetAllProductFilters());
        }

        [HttpGet("product/test")]
        public JsonResult Test() {
            FilterRequest r = new FilterRequest();

            List<int> a = new List<int>();
            List<int> b = new List<int>();

            a.Add(1);
            a.Add(2);

            b.Add(0);

            r.Values.Add(0, b);
            r.Values.Add(1, b);

            return Json(r);
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
