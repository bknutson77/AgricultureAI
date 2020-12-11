using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgricultureAI
{
    public class Program
    {
        /// <summary>
        /// This is the main entry point of the program, and the first method called. Here we create a host builder.
        /// </summary>
        /// <param name="args">A string of command line arguments.</param>
        public static void Main(string[] args)
        {
            // Create Website and Launch:
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method to create a host builder (an engine to configure the website before launching, which calls logic in Startup.cs).
        /// </summary>
        /// <param name="args">A string of command line arguments.</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseWebRoot("Website");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
