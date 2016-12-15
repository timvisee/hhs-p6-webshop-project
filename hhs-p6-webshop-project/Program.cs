using System.IO;
using hhs_p6_webshop_project.App.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace hhs_p6_webshop_project {
    public class Program {

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
        }
    }
}