using System;
using System.Linq;
using Xunit;

namespace hhs_p6_webshop_project_test {

    [Collection("main")]
    public class DatabaseContextTests {

        /// <summary>
        /// Database fixture instance, used for testing.
        /// </summary>
        private DatabaseFixture _databaseFixture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseFixture">Database fixture.</param>
        public DatabaseContextTests(DatabaseFixture databaseFixture) {
            this._databaseFixture = databaseFixture;
        }

        /// <summary>
        /// Ensure the database test context isn't null.
        /// </summary>
        [Fact]
        public void ValidateDatabaseContext() {
            Assert.NotNull(this._databaseFixture.Context);
        }
    }
}