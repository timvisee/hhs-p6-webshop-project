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
        public static void Main(string[] args) {
#if DEBUG
            Console.WriteLine("Reset all data sources? Press any key to confirm...");

            int count = 5;

            Console.Write("Process will continue in " + count);

            while (count > 0) {
                Thread.Sleep(1000);

                if (Console.KeyAvailable) {
                    ApplicationDbContext.reset = true;
                    Console.WriteLine();
                    Console.WriteLine("Database will be rebuilt.");
                    break;
                }

                Console.CursorLeft--;
                count--;
                Console.Write(count);
            }

            if (!ApplicationDbContext.reset)
                Console.WriteLine();

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