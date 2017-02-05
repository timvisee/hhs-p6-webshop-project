using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using hhs_p6_webshop_project.Controllers.ProductControllers;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.ProductModels;
using Xunit;
using Microsoft.EntityFrameworkCore;
using hhs_p6_webshop_project.Models;
using System.Security.Claims;
using hhs_p6_webshop_project_test.Roderick;
using Microsoft.AspNetCore.Mvc;

namespace hhs_p6_webshop_project_test.ProductTest
{
    public class ProductImagesControllerTest
    {
        private List<ProductImage> GetTestProductImages()
        {
            List<ProductImage> ProductImages = new List<ProductImage>();
            ProductImages.Add(new ProductImage()
            {
                ProductImageId = 1,
                ColorOptionId = 1,
                Path = "~/images/uploads"
            });

            return ProductImages;
        }

        [Fact]
        public void changePathNameTest_PathWillBeDifferent()
        {
            //  Arrange
            var dataSource = new Mock<ApplicationDbContext>();
            var temp = Tools.GetQueryableMockDbSet<ProductImage>(GetTestProductImages());
            dataSource.Setup(m => m.ProductImages).Returns(temp);
            var controller = new ProductImagesController(dataSource.Object);
            
            // Act

            var testMethod = controller.ChangePathName(dataSource.Object.ProductImages.Select(m => m.Path).ToString());

            // Assert

            Assert.NotEqual(dataSource.Object.ProductImages.Select(m => m.Path).ToString(), testMethod);
        }

        [Fact]
        public void detailsPage_NotAuthenticated()
        {
            //Arrange
            var dataSource = new Mock<ApplicationDbContext>();
            var temp = Tools.GetQueryableMockDbSet<ProductImage>(GetTestProductImages());
            dataSource.Setup(m => m.ProductImages).Returns(temp);
            var controller = new ProductImagesController(dataSource.Object);
            var User = new ClaimsIdentity("Admin");

            //Act
            int? testValue = dataSource.Object.ProductImages.Select(m => m.ProductImageId).FirstOrDefault();
            var methodToTest = controller.Details(testValue);

            //Assert
            Assert.IsType<ViewResult>(methodToTest);
        }
    }
}
