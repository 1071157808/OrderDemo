using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using OrderGrain;

namespace OrderDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();


            var host = new HostBuilder()
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.UseUrls("http://*:8080");
                  // Configure ASP.NET Core
                  webBuilder.UseStartup<Startup>();
              })
              .UseOrleans(builder =>
              {
                  builder.ConfigureApplicationParts(manager =>
                  {
                      manager.AddApplicationPart(typeof(IProductGrain).Assembly).WithReferences();
                      manager.AddApplicationPart(typeof(IOrderGrain).Assembly).WithReferences();
                  });
                  builder.UseLocalhostClustering();
                  builder.AddMemoryGrainStorageAsDefault();
              })
              .ConfigureLogging(logging =>
              {
                  /* Configure cross-cutting concerns such as logging */
              })
              .ConfigureServices(services =>
              {
                  /* Configure shared services */
              })
              .UseConsoleLifetime()
              .Build();

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            
    }
}
