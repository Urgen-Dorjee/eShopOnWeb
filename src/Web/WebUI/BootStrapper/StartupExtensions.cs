using eWebShop.Persistence.Auth;
using eWebShop.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace eWebShop.WebUI.BootStrapper
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSqlServerDatabase(this IServiceCollection services, IConfiguration configure)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<CatalogContext>(options =>
                   {
                       options.UseSqlServer(configure["CatalogConnection"],
                           sqlServerOptionsAction: resilient =>
                           {
                               //resilient.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               resilient.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });

                   }, ServiceLifetime.Scoped);

            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<AppIdentityDbContext>(options =>
                    {
                        options.UseSqlServer(configure["UserConnection"],
                        sqlServerOptionsAction: resilient =>
                        {
                            resilient.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });
                    }, ServiceLifetime.Scoped);

            CreateIdentityDatabase(services);
                     
            return services;
        }

        private static void CreateIdentityDatabase(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using(var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<ApplicationUser>>();
                if (existingUserManager == null)
                {
                    services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddDefaultUI(UIFramework.Bootstrap4)
                            .AddEntityFrameworkStores<AppIdentityDbContext>()
                            .AddDefaultTokenProviders();
                }
            }
        }
    }
}
