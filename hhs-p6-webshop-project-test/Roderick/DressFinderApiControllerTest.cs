using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Api;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using hhs_p6_webshop_project.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace hhs_p6_webshop_project_test.Roderick
{
    public class DressFinderApiControllerTest
    {
        private DressFinderApiController CreateDressFinderApiControllerInstance()
        {
            //Create a mocked product service
            var mockService = new Mock<IProductService>();

            //Set it up to return the list of test products from the database builder
            mockService.Setup(m => m.GetAllProducts()).Returns(DbBuilder.GenerateProductsAsList);
            mockService.Setup(m => m.Filter(null)).Returns(DbBuilder.GenerateProductsAsList);

            //Create the instance of the actual api controller
            return new DressFinderApiController(mockService.Object);
        }

        [Fact]
        public void TestGetAllProducts()
        {
            //Get an instance of the actual api controller
            var apiController = CreateDressFinderApiControllerInstance();

            //Calculate the actual return value
            var returnValue = apiController.GetAllProducts();

            //Calculate the expected return value
            var expectedValue = new JsonResult(DbBuilder.GenerateProductsAsList());

            //Check if its result matches the expected output
            Assert.Equal(JsonConvert.SerializeObject(returnValue), JsonConvert.SerializeObject(expectedValue));
        }

        [Fact]
        public void TestFilterPartialSort_IllegalValue()
        {
            //Get an instance of the actual api controller
            var apiController = CreateDressFinderApiControllerInstance();

            //Calculate the actual return value, null is ok because this means that no filters are selected
            var returnValue = apiController.FilterPartialSort(null, -1);

            //Check if we got a BadRequest object
            Assert.IsType<BadRequestObjectResult>(returnValue);
        }

        [Fact]
        public void TestFilterPartialSort_LegalValue()
        {
            //Get an instance of the actual api controller
            var apiController = CreateDressFinderApiControllerInstance();

            //Calculate the actual return value, null is ok because this means that no filters are selected
            var returnValue = apiController.FilterPartialSort(null, 0);

            //Check if we got a proper partial view result
            Assert.IsType<PartialViewResult>(returnValue);
        }
    }
}
