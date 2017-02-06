using System.Linq;
using hhs_p6_webshop_project.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace hhs_p6_webshop_project_test
{
    public class DbTest
    {

        [Fact]
        public void TestDb() {
            //Setup data

            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "db_test")
                .Options;

            var dbOptions2 = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "db_test2")
                .Options;

            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(dbOptions))
            {
                DbBuilder.GenerateProducts(context);

                using (var context2 = new ApplicationDbContext(dbOptions2)) {
                    DbBuilder.GenerateProducts(context2);

                    foreach (var p in context2.Products) 
                     Assert.NotNull(context.Products.Where(prod => prod.ProductId == p.ProductId));
                }

                
            }

            dbOptions2 = null;
            dbOptions = null;


        }
    }
}
