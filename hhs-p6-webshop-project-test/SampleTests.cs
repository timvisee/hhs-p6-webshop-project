using System;
using Xunit;

namespace hhs_p6_webshop_project_test {

    [Collection("main")]
    public class SampleTests {

        /// <summary>
        /// Database fixture instance, used for testing.
        /// </summary>
        private DatabaseFixture _databaseFixture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseFixture">Database fixture.</param>
        public SampleTests(DatabaseFixture databaseFixture) {
            this._databaseFixture = databaseFixture;
        }

        [Fact]
        public void SampleTest() {}
    }
}