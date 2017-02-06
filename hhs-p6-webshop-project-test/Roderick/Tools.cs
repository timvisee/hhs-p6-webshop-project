using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.Controllers;
using hhs_p6_webshop_project.Controllers.NewsControllers;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.AppointmentModels;
using hhs_p6_webshop_project.Models.NewsModels;
using hhs_p6_webshop_project.Services.Abstracts;
using hhs_p6_webshop_project.Services.Containers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace hhs_p6_webshop_project_test.Roderick
{
    public class Tools
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);

            return dbSet.Object;
        }

        public static AppointmentsController CreateControllerInstance(bool isAuthenticated = false)
        {
            var httpContext = MockHttpContext(isAuthenticated);
            var dbContext = MockTestDatabaseContext();
            var config = MockTestConfig();
            var service = MockEmailService();

            return new AppointmentsController(dbContext, config, service)
            {
                ControllerContext = new ControllerContext()
                {
                    //Override http context, to return the proper user
                    HttpContext = httpContext
                }
            };
        }

        public static NewsCategoriesController CreateNewsCategoriesControllerInstance(bool isAuthenticated = false)
        {
            var httpContext = MockHttpContext(isAuthenticated);
            var dbContext = MockTestDatabaseContext();

            return new NewsCategoriesController(dbContext)
            {
                ControllerContext = new ControllerContext()
                {
                    //Override http context, to return the proper user
                    HttpContext = httpContext
                }
            };
        }

        public static HttpContext MockHttpContext(bool isAuthenticated)
        {
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(MockTestUser(isAuthenticated));

            return mockHttpContext.Object;
        }

        public static ITransactionalEmailService MockEmailService()
        {
            var mockService = new Mock<ITransactionalEmailService>();
            mockService.Setup(s => s.SendAppointmentEmail(It.IsAny<AppointmentMessageContainer>()));

            return mockService.Object;
        }

        public static ClaimsPrincipal MockTestUser(bool isAuthenticated)
        {
            var mockUser = new Mock<ClaimsPrincipal>();
            var mockIdentity = new Mock<IIdentity>();

            mockIdentity.Setup(i => i.IsAuthenticated).Returns(isAuthenticated);
            mockUser.Setup(u => u.Identity).Returns(mockIdentity.Object);

            return mockUser.Object;
        }

        public static ApplicationDbContext MockTestDatabaseContext()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Appointment).Returns(GetQueryableMockDbSet(CreateTestAppointments()));
            mockContext.Setup(c => c.NewsCategory).Returns(GetQueryableMockDbSet(CreateTestCategory()));
            mockContext.Setup(c => c.Products).Returns(GetQueryableMockDbSet(DbBuilder.GenerateProductsAsList()));
            return mockContext.Object;
        }

        public static IOptions<SecureAppConfig> MockTestConfig()
        {
            var mockOptions = new Mock<IOptions<SecureAppConfig>>();
            var mockConfig = new Mock<SecureAppConfig>();
            mockConfig.Setup(c => c.SparkpostApiKey).Returns("<empty>");

            mockOptions.Setup(o => o.Value).Returns(mockConfig.Object);

            return mockOptions.Object;
        }

        public static List<Appointment> CreateTestAppointments()
        {
            return new List<Appointment>()
            {
                new Appointment()
                {
                    AppointmentDateTime = DateTime.Now,
                    Confirmation = true,
                    DateMarried = DateTime.Now,
                    ID = 1,
                    Mail = "fake@fake.com",
                    Name = "Name",
                    Phone = "+000000000"
                }
            };
        }

        public static List<NewsCategory> CreateTestCategory()
        {
            return new List<NewsCategory>()
            {
                new NewsCategory()
                {
                    Name = "Nieuws"
                }
            };
        }
    }
}