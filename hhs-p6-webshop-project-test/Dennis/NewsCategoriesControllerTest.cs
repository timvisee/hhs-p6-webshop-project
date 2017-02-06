using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using hhs_p6_webshop_project.Controllers.NewsControllers;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.NewsModels;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project_test.Roderick;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;


namespace hhs_p6_webshop_project_test.Dennis
{
    public class NewsCategoriesControllerTest
    {

        [Fact]
        public void CreateInNewsCategoriesController()
        {
            // Create a NewsCategoryView (for the create function)
            NewsCategoryView na = new NewsCategoryView()
            {
                NewsCategory = new NewsCategory()
                {
                    Name = "Nieuws"
                }
            };
            
            // Mocking
            var dbContext = Tools.MockTestDatabaseContext();
            var controller = new NewsCategoriesController(dbContext);

            // Create a category
            var result = controller.Create(na);
            
            // Check if the result is a redirect
            Assert.IsType<RedirectToActionResult>(result);

            // Check if the name is saved correct
            Assert.Contains(na.NewsCategory.Name, dbContext.NewsCategory.FirstOrDefault().Name);
        }

        [Fact]
        public void CreateViewInNewsCategoriesController_NotLoggedIn()
        {
            // Initialize the controller instance, with a user which is not logged in.
            var controller = Tools.CreateNewsCategoriesControllerInstance();

            // Create the create news category view
            var cView = controller.Create();

            // User is not able to create a category because it's not logged in
            // Test for a 404 error
            Assert.IsType<NotFoundResult>(cView);
        }
    }
}
