using Xunit;

namespace hhs_p6_webshop_project_test {

    [CollectionDefinition("main")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }
}