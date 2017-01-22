using System;
using hhs_p6_webshop_project.Data;

namespace hhs_p6_webshop_project_test {
    public class DatabaseFixture : IDisposable {

        /// <summary>
        /// Application database context using the test set up.
        /// </summary>
        public ApplicationDbContext Context { get; }

        /// <summary>
        /// Initialize the test database fixture.
        /// </summary>
        public DatabaseFixture() {
            // Create a new application db context
            this.Context = new ApplicationDbContext();

            // Build the database for testing
            DbBuilder.Rebuild(Context);
        }

        /// <summary>
        /// Dispose the test database fixture when done.
        /// </summary>
        public void Dispose() {
            // Delete the test database
            Console.WriteLine("Deleting used test database...");
            this.Context.Database.EnsureDeleted();
            Console.WriteLine("Test database deleted!");
        }

    }
}