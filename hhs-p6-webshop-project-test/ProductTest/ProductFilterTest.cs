using System.Collections.Generic;
using System.Linq;
using hhs_p6_webshop_project.Data;
using hhs_p6_webshop_project.Models.FilterModels;
using hhs_p6_webshop_project.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace hhs_p6_webshop_project_test.ProductTest
{
    public class ProductFilterTest
    {

        [Fact]
        public void TestFilter() {
            //Setup data

            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "filter_test")
                .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(dbOptions))
            {
                DbBuilder.GenerateProducts(context);

                var productService = new ProductService(context);

                //Create filters
                var filters = new List<FilterBase>();
                filters.Add(new ColorFilter(new string[] { "Ivoor" }));
                filters.Add(new PriceFilter(1000d, 1500d));

                

                List<string> expectedResults = new List<string>();
                expectedResults.Add(context.Products.First( p=> p.Name == "Ladybird" && (int)p.Price == 1250).ToString());
                expectedResults.Add(context.Products.First( p=> p.Name == "Jarice" && (int)p.Price == 1500).ToString());

                var result = productService.Filter(filters);
                
                foreach (var p in result) 
                     Assert.Contains(p.ToString(), expectedResults);
            }
            

        }



    }
}
