using System.Collections.Generic;
using hhs_p6_webshop_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project.Api
{
    [Route("api/dressfinder")]
    public class DressFinderController : Controller
    {
        public IProductService ProductService { get;set; }

        public DressFinderController(IProductService productService) {
            ProductService = productService;
        }

        [HttpGet("product/all")]
        public JsonResult GetAllProducts() {
            return Json(ProductService.GetAllProducts());
        }

        [HttpGet("product/test")]
        public JsonResult GetAll() {
            return Json(ProductService.Test());
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
