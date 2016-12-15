using System;
using System.Linq;
using System.Threading;
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
            if (args.Any(e => e == "--force-database-initialization")) {
                // Show a status message
                LogUtils.Warning("-------------------------------");
                LogUtils.Warning("| Database rebuild is forced!  |");
                LogUtils.Warning("| ALL DATA WILL BE LOST!       |");
                LogUtils.Warning("-------------------------------");

                // Set the database reset flag
                config.DatabaseReset = true;

            } else if(args.All(e => e != "--always-skip-initialization")) {
                // Ask the user to reset the database
                // TODO: Move this logic to another class. Only parse arguments here.
                LogUtils.Info("Press any key to reset the database... (disable using --always-skip-initialization argument)");

                // Ask the user to press a key if the database should be reset
                // TODO: Move the number of seconds to a constant.
                if (AskUserPressKey(3)) {
                    // Show a warning to the user
                    LogUtils.Warning("");
                    LogUtils.Warning("-------------------------------");
                    LogUtils.Warning("| Database will be rebuilt.    |");
                    LogUtils.Warning("| ALL DATA WILL BE LOST!       |");
                    LogUtils.Warning("-------------------------------");

                    // Set the database reset flag, and break the waiting loop
                    config.DatabaseReset = true;
                }
            }

            // Set whether to exit the application after the database is reset
            if (args.Any(e => e == "--exit-after-initialization")) {
                // Tell the user the application will exit after database reset
                LogUtils.Warning("The application will quit after the database has been reset.");

                // Set the exit flag
                config.DatabaseExitAfterReset = true;
            }
        }

        /// <summary>
        /// Ask the user to press a key.
        ///
        /// Note: False is also returned if no input stream (for key presses) is available.
        /// </summary>
        /// <param name="seconds">Number of seconds to allow the user to press a key.</param>
        /// <returns>True if a key was pressed within the specified time, false if not.</returns>
        private static bool AskUserPressKey(int seconds) {
            // Make sure an input stream is available
            if (Console.In == null)
                return false;

            // Test whether checking if a key is available succeeds
            try {
                var keyAvailable = Console.KeyAvailable;

            } catch (System.InvalidOperationException ex) {
                Console.WriteLine("User input not supported in this mode, skipping...");
                return false;
            }

            // Show a status message
            Console.Write("Continuing in " + seconds + "...");
            Console.CursorLeft = Math.Max(Console.CursorLeft - 4, 0);

            // Loop until the number of seconds is passed
            while (seconds > 0) {
                // Sleep the thread for a second, to count down once a second
                Thread.Sleep(1000);

                // Check whether a key is pressed by the user
                if (Console.KeyAvailable)
                    return true;

                // Update the time
                seconds--;

                // Update the time in the console
                Console.Write(seconds);
                Console.CursorLeft = Math.Max(Console.CursorLeft - 1, 0);
            }

            // Remove the timer
            Console.CursorLeft = 0;
            Console.WriteLine("No user input received in time.\n");

            // No key seems to have been pressed, return false
            return false;
        }
    }
}