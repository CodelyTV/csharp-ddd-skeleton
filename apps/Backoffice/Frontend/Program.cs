using System;
using System.IO;
using System.Linq;
using CodelyTv.Apps.Backoffice.Frontend.Command;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CodelyTv.Apps.Backoffice.Frontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any()) CreateWebHostBuilder(args).Build().Run();

            BackofficeFrontendCommandBuilder.Create(args).Build(Configuration()).Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }

        private static IConfigurationRoot Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }
    }
}
