using System;

namespace hhs_p6_webshop_project.App.Util {
    public static class DbUtils {

        /// <summary>
        /// Get the connection string to use for the database connection.
        /// </summary>
        /// <returns>Connection string that should be used for the database connection.</returns>
        public static String getConnectionString() {
            // return Program.Configuration.GetConnectionString("DefaultConnection");
            throw new Exception("Connection string fetching not implemented yet.");
        }

    }
}