using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using hhs_p6_webshop_project.Controllers.NewsControllers;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.NewsModels;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace hhs_p6_webshop_project_test.NewsTests
{
    public class NewsCategoriesControllerTest
    {

        [Fact]
        public void CreateInNewsCategoriesController()
        {
            // Creat a new NewsCategory(view)
            NewsCategoryView na = new NewsCategoryView()
            {
                NewsCategory = new NewsCategory()
                {
                    Name = "Nieuws"
                }
            };

            // Set up the mocking
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockDbSetNewsCategory = new Mock<DbSet<NewsCategory>>();

            mockDbContext.Setup(x => x.NewsCategory).Returns(mockDbSetNewsCategory.Object);
            NewsCategoriesController c = new NewsCategoriesController(mockDbContext.Object);

            // Create the new news category
            c.Create(na);
            
            // TODO: Test is failing because the add was never performed?
            // Verify
            mockDbSetNewsCategory.Verify(m => m.Add(It.Is<NewsCategory>(n => n.Name == "Nieuws")));
            mockDbContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateViewInNewsCategoriesController() {
            // Creat a new NewsCategory(view)
            NewsCategoryView na = new NewsCategoryView()
            {
                NewsCategory = new NewsCategory()
                {
                    Name = "Nieuws"
                }
            };

            // Set up the mocking
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockDbSetNewsCategory = new Mock<DbSet<NewsCategory>>();

            mockDbContext.Setup(x => x.NewsCategory).Returns(mockDbSetNewsCategory.Object);
            NewsCategoriesController c = new NewsCategoriesController(mockDbContext.Object);

            // Render the create news category page
            var cView = c.Create();

            // TODO: returns an redirecttoaction????
            // Test for a 404 error
            Assert.IsType<NotFoundResult>(cView);
        }
        
    }
}
