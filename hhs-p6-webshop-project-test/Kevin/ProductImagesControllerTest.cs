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
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

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
                Path = "~/images/uploads",
                ColorOption = new ColorOption()
                {
                    ColorOptionId = 1,
                    Color = "Ivoor",
                    ProductId = 1,
                    Product = new Product()
                    {
                        ProductId = 1,
                        Name = "Test",
                        Description = "Test",
                        Price = 2500
                    }
                }
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
        public void detailsPage_NotAuthenticated_Null()
        {
            var controller = createMockedController(false);

            //Act
            var result = controller.Details(null);
       
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void detailsPage_Authenticated_Null()
        {
            var controller = createMockedController(true);

            //Act
            var result = controller.Details(null);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void detailsPage_NotAuthenticated_CorrectNumber()
        {
            var controller = createMockedController(false);

            //Act
            var result = controller.Details(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void indexPage_NotAuthenticated()
        {
            var controller = createMockedController(false);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void deletePage_Authenticated_Null()
        {
            var controller = createMockedController(true);

            //Act
            var result = controller.Delete(null);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void deletePage_NotAuthenticated_NormalValue()
        {
            var controller = createMockedController(false);

            //Act
            var result = controller.Delete(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private ProductImagesController createMockedController(bool isAuthenticated)
        {
            //Arrange
            var dataSource = new Mock<ApplicationDbContext>();
            var temp = Tools.GetQueryableMockDbSet<ProductImage>(GetTestProductImages());
            dataSource.Setup(m => m.ProductImages).Returns(temp);
            var controller = new ProductImagesController(dataSource.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = Tools.MockHttpContext(isAuthenticated)
                }
            };

            return controller;
        }
    }
}
