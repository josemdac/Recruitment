using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace RenovaHrEmploymentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder => 
               {
                   webBuilder.UseStartup<Startup>();
                   var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                   if (env == "Development")
                   {
                       webBuilder.UseUrls("https://localhost:5005");
                   }
               });
    }
}
