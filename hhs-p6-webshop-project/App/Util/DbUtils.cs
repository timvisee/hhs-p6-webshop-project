using System;
using Microsoft.Extensions.Configuration;

namespace hhs_p6_webshop_project.App.Util
{
    public static class DbUtils
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public const String EnvDbConnectionString = "DB_CONNECTION_STRING";

        /// <summary>
        /// Get the connection string to use for the database connection.
        /// </summary>
        /// <returns>Connection string that should be used for the database connection.</returns>
        public static String GetConnectionString()
        {
            // Get the environment variable
            var dbConStr = Environment.GetEnvironmentVariable(EnvDbConnectionString);

            // Return the environment connection string if available
            if (!string.IsNullOrEmpty(dbConStr))
                return dbConStr;

            // Make sure the file configuration is available
            if (Program.FileConfig == null)
                throw new Exception("Failed to determine database connection string. Configuration not loaded yet.");

            // Return the connection string from the configuration file
            return Program.FileConfig.GetConnectionString("DefaultConnection");
        }

        public static string GetAndInjectConnectionString()
        {
            if (!IsRemote()) //Don't inject anything if we're using a local db
                return GetConnectionString();

            string connString = GetConnectionString();

            connString = connString.Replace("*|SERVER|*", Program.FileConfig["DbServer"]);
            connString = connString.Replace("*|DATABASE|*", Program.FileConfig["DbName"]);
            connString = connString.Replace("*|USER|*", Program.FileConfig["DbUser"]);
            connString = connString.Replace("*|PASS|*", Program.FileConfig["DbPassword"]);

            return connString;
        }

        /// <summary>
        /// Check whether the database connection is remote.
        /// </summary>
        /// <returns>True if remote, false if not.</returns>
        public static bool IsRemote()
        {
            // TODO: Do not automatically detect this, as there isn't a rock-solid way to do this.
            return !GetConnectionString().Contains(".db") && !GetConnectionString().Contains(".sqlite");
        }
    }
}