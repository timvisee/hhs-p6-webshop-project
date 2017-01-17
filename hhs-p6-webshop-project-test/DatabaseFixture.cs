using System;
using hhs_p6_webshop_project;
using hhs_p6_webshop_project.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace hhs_p6_webshop_project_test {
    public class DatabaseFixture : IDisposable {

        public ApplicationDbContext Context { get; }

        public DatabaseFixture() {
            // Create a new application db context
            this.Context = new ApplicationDbContext();

            // Build the database for testing
            DbBuilder.Rebuild(Context);
        }

        public void Dispose() {
            // TODO: Clean up the test data!
        }

    }
}