using System;
using System.IO;
using System.Linq;
using System.Threading;
using hhs_p6_webshop_project.App.Util;
using hhs_p6_webshop_project.Data;
using Microsoft.AspNetCore.Hosting;

namespace hhs_p6_webshop_project {
    public class Program {

        public static void DebugInitialize(bool force = false) {
            if (force) {
                DbInitializer.Run = true;
                LogUtils.Log("-------------------------------");
                LogUtils.Log("| Forcing database rebuild!    |");
                LogUtils.Log("| ALL DATA WILL BE LOST!       |");
                LogUtils.Log("-------------------------------");
                return;
            }

            Console.WriteLine("This prompt can be disabled by adding --always-skip-initialization as a startup argument.");
            Console.WriteLine("Reset all data sources? Press any key to confirm...");

            int count = 2;

            Console.Write("Process will continue in " + count);

            while (count > 0)
            {
                Thread.Sleep(1000);

                if (Console.KeyAvailable) {
                    DbInitializer.Run = true;
                    Console.WriteLine();
                    LogUtils.Log("-------------------------------");
                    LogUtils.Log("| Database will be rebuilt.    |");
                    LogUtils.Log("| ALL DATA WILL BE LOST!       |");
                    LogUtils.Log("-------------------------------");
                    break;
                }

                Console.CursorLeft--;
                count--;
                Console.Write(count);
            }

            if (!DbInitializer.Run)
                Console.WriteLine("\nDatabase will NOT be rebuilt.");
        }

        public static void Main(string[] args) {

            bool force = args.Any(e => e == "--force-database-initialization");
            if (force) {
                //Force database migration
                DebugInitialize(true);
                
            }

#if DEBUG
            bool doNotInitialize = args.Length > 0 && args[0] == "--always-skip-initialization";
            
            if (!doNotInitialize && !force) {
                DebugInitialize();
            }
#endif

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            if (args.Any(e => e == "--exit-after-initialization"))
            {
                //Database migration only, so kill the process
                Console.WriteLine("Only a database reconstruction was requested. Terminating application...");
                return;

            }

            host.Run();
        }
    }
}