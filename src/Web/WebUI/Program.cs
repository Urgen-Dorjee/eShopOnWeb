#region Serilog
//using eWebShop.Persistence.Auth;
//using eWebShop.Persistence.Data;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Serilog;
//using System;
//using System.IO;

//namespace eWebShop.WebUI
//{
//    public class Program
//    {
//        public static readonly string NameSpace = typeof(Program).Namespace;
//        public static readonly string AppName = NameSpace.Substring(NameSpace.LastIndexOf('.', NameSpace.LastIndexOf('.') - 1) + 1);
//        public static int Main(string[] args)
//        {
//            var configuration = GetConfiguration();
//            Log.Logger = CreateSeriLogger(configuration);

//            try
//            {
//                Log.Information("Configuring Web Host ({ApplicationContext})...", AppName);
//                var host = CreateWebHostBuilder(configuration, args);

//                Log.Information("Applying Migrations.. ({ApplicationContext})", AppName);
//                host.MigrateDbContext<CatalogContext>((context, services) =>
//                {
//                    var env = services.GetService<IHostingEnvironment>();
//                    var logger = services.GetService<ILogger<CatalogContextSeed>>();
//                    CatalogContextSeed.SeedAsync(context, logger).Wait();
//                });               

//                Log.Information("Starting web host ({ApplicationContext})...", AppName);
//                host.Run();

//                return 0;
//            }
//            catch (Exception ex)
//            {
//                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
//                return 1;
//            }
//            finally
//            {
//                Log.CloseAndFlush();
//            }
//        }

//        private static Serilog.ILogger CreateSeriLogger(IConfiguration configuration)
//        {
//            return new LoggerConfiguration()
//                   .MinimumLevel.Verbose()
//                   .Enrich.WithProperty("ApplicationContext", AppName)
//                   .Enrich.FromLogContext()
//                   .WriteTo.Console()
//                   .ReadFrom.Configuration(configuration)
//                   .CreateLogger();
//        }

//        private static IConfiguration GetConfiguration()
//        {
//            var builder = new ConfigurationBuilder()
//                             .SetBasePath(Directory.GetCurrentDirectory())
//                             .AddJsonFile("appsettings.json", false, true)
//                             .AddEnvironmentVariables();
//            return builder.Build();
//        }

//        public static IWebHost CreateWebHostBuilder(IConfiguration configuraton, string[] args) =>
//                WebHost.CreateDefaultBuilder(args)
//                      .UseStartup<Startup>()
//                      .UseApplicationInsights()
//                      .UseSerilog()
//                      .Build();
//    }
//}
#endregion

using eWebShop.Persistence.Auth;
using eWebShop.Persistence.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Serilog;
using System;
using System.Threading.Tasks;

namespace eWebShop.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await Policy.Handle<Exception>()
                          .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) =>
                          {
                              Log.Error("Error connecting to SQL Server. Retrying in 5 sec.");
                          }).Execute(async () =>
                          {
                              var logger = services.GetService<ILogger<CatalogContextSeed>>();
                              var catalogContext = services.GetRequiredService<CatalogContext>();
                              await CatalogContextSeed.SeedAsync(catalogContext, logger);

                              var appUserContext = services.GetRequiredService<AppIdentityDbContext>();                             
                              var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                              var contextLogger = services.GetService<ILogger<AppIdentityDbContextSeed>>();
                              await AppIdentityDbContextSeed.SeedAsync(appUserContext,userManager, contextLogger);
                          });
                   
                }
                catch (Exception ex)
                {
                    var logger = services.GetService<ILogger<Program>>();
                    logger.LogError("Program could not seed the data and terminated unexpectedly", ex.Message);
                }
            }
            host.Run();
        }      

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                      .UseStartup<Startup>()
                      .UseApplicationInsights()
                      .UseSerilog();
    }
}
