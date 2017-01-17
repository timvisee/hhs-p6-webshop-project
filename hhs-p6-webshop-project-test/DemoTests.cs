using System;
using Xunit;

namespace hhs_p6_webshop_project_test {

    [Collection("main")]
    public class DemoTests {

        /// <summary>
        /// Database fixture instance, used for testing.
        /// </summary>
        private DatabaseFixture _databaseFixture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseFixture">Database fixture.</param>
        public DemoTests(DatabaseFixture databaseFixture) {
            this._databaseFixture = databaseFixture;
        }

        [Fact]
        public void SampleTest() {
            Console.WriteLine("GETTING DATA FROM DB: ");
//            Console.WriteLine(this._databaseFixture.Context.Appointment.ToList());
        }
    }
}