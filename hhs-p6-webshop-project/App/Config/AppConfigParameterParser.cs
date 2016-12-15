using System;
using System.Threading;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Data;

namespace hhs_p6_webshop_project.App.Config {
    public static class AppConfigParameterParser {

        /// <summary>
        /// Parse the given arguments, and apply it to the given configuration.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        /// <param name="config">Configuration to apply the parsed parameters to.</param>
        public static void Parse(string[] args, AppConfig config) {
            // Check whether the database should reset
            if (args.Any(e => e == "--force-database-initialization")) {
                // Show a status message
                LogUtils.Warning("-------------------------------");
                LogUtils.Warning("| Database rebuild is forced!  |");
                LogUtils.Warning("| ALL DATA WILL BE LOST!       |");
                LogUtils.Warning("-------------------------------");

                // Set the database reset flag
                config.DatabaseReset = true;

            } else if(!args.Any(e => e == "--always-skip-initialization")) {
                // TODO: Clean this section up!
                // TODO: Move this user input section to a separate method.

                LogUtils.Info("This prompt can be disabled by adding --always-skip-initialization as a startup argument.");
                LogUtils.Info("Reset all data sources? Press any key to confirm...");

                // Number of seconds to wait for user input
                int waitSeconds = 3;

                LogUtils.Info("Process will continue in " + waitSeconds);

                while (Console.In != null && waitSeconds > 0) {
                    // Sleep the thread for a second, to count down once a second
                    Thread.Sleep(1000);

                    // Check whether a key is pressed by the user
                    if (Console.KeyAvailable) {
                        // Show a warning to the user
                        LogUtils.Warning("");
                        LogUtils.Warning("-------------------------------");
                        LogUtils.Warning("| Database will be rebuilt.    |");
                        LogUtils.Warning("| ALL DATA WILL BE LOST!       |");
                        LogUtils.Warning("-------------------------------");

                        // Set the database reset flag, and break the waiting loop
                        config.DatabaseReset = true;
                        break;
                    }

                    // Replace
                    Console.CursorLeft--;
                    waitSeconds--;
                    Console.Write(waitSeconds);
                }

                // Show a message when the database won't rebuilt
                if (!config.DatabaseReset)
                    LogUtils.Info("\nDatabase will NOT be rebuilt.");
            }

            // Set whether to exit the application after the database is reset
            if (args.Any(e => e == "--exit-after-initialization")) {
                // Tell the user the application will exit after database reset
                LogUtils.Warning("The application will quit after the database has been reset.");

                // Set the exit flag
                config.DatabaseExitAfterReset = true;
            }
        }
    }
}