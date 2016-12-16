using System;
using System.IO;
using hhs_p6_webshop_project.App.Config;
using hhs_p6_webshop_project.App.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace hhs_p6_webshop_project {
    public class Program {

        /// <summary>
        /// Application name.
        /// </summary>
        public const String APP_NAME = "Honeymoon Serer";

        /// <summary>
        /// Application version code.
        /// </summary>
        public const String APP_VERSION_NAME = "0.0.1-dev";

        /// <summary>
        /// Application version code.
        /// </summary>
        public const int APP_VERSION_CODE = 1;

        /// <summary>
        /// Application configuration.
        /// This configuration is used throughout the application to define the applications behaviour.
        /// </summary>
        public static AppConfig AppConfig;

        /// <summary>
        /// Base application configuration.
        /// </summary>
        public static IConfigurationRoot FileConfig { get; set; }

        /// <summary>
        /// Main application entry point.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        public static void Main(string[] args) {
            // Show a program initialization message
            LogUtils.Info("Starting " + APP_NAME + " v" + APP_VERSION_NAME + " (" + APP_VERSION_CODE + ")...");

#if DEBUG
            //Enable development environment and debugging features
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            LogUtils.Info("Enabled development environment.");
#endif

            try {
                // Initialize the application configuration
                Program.AppConfig = new AppConfig(true);

                // Parse the startup arguments, and apply them to the configuration
                AppConfigParameterParser.Parse(args, Program.AppConfig);

                // Set up a new webhost
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                // Run the actual web host
                host.Run();

            } catch (Exception ex) {
                // Print the exception
                Console.WriteLine(ex);

                // Show a warning
                Console.WriteLine("\n\nAn unrecorerable exception occurred.\nThe application will not quit (code: -1).");

                // Exit
                Environment.Exit(-1);
            }

        }
    }
}