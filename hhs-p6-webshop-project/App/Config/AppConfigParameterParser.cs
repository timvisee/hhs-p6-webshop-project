using System.Linq;
using hhs_p6_webshop_project.App.Util;

namespace hhs_p6_webshop_project.App.Config {
    public static class AppConfigParameterParser {

        /// <summary>
        /// Parse the given arguments, and apply it to the given configuration.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        /// <param name="config">Configuration to apply the parsed parameters to.</param>
        public static void Parse(string[] args, AppConfig config) {
            // Check whether the database should reset
            if (args.Any(e => e == "--db-init")) {
                // Show a status message
                LogUtils.Warning("Database initialization is invoked using --db-init");

                // Set the database reset flag
                config.DatabaseReset = true;
            }

            // Set whether to exit the application after the database is reset
            if (args.Any(e => e == "--db-init-exit")) {
                // Tell the user the application will exit after database reset
                LogUtils.Warning("The application will exit after database initialization because of --db-init-exit");

                // Set the exit flag
                config.DatabaseExitAfterReset = true;
            }
        }
    }
}