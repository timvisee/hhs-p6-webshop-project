using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using hhs_p6_webshop_project.Data;
using Microsoft.AspNetCore.Hosting;

namespace hhs_p6_webshop_project {
    public class Program {
        public static void Log(string text, ConsoleColor color = ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DebugSettings() {
            Console.WriteLine("This prompt can be disabled by adding --always-skip-initialization as a startup argument.");
            Console.WriteLine("Reset all data sources? Press any key to confirm...");

            int count = 5;

            Console.Write("Process will continue in " + count);

            while (count > 0)
            {
                Thread.Sleep(1000);

                if (Console.KeyAvailable) {
                    DbInitializer.Run = true;
                    Console.WriteLine();
                    Program.Log("-------------------------------");
                    Program.Log("| Database will be rebuilt.    |");
                    Program.Log("| ALL DATA WILL BE LOST!       |");
                    Program.Log("-------------------------------");
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

            bool doNotInitialize = args.Length > 0 && args[0] == "--always-skip-initialization";

#if DEBUG
            if (!doNotInitialize) {
                DebugSettings();
            }
#endif

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();


        }
    }
}