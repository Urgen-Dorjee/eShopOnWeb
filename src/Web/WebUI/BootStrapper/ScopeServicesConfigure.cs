using eWebShop.Application;
using eWebShop.Application.Contracts;
using eWebShop.Application.Services;
using eWebShop.Persistence.Infrastructure;
using eWebShop.Persistence.Infrastructure.Logging;
using eWebShop.Persistence.Services;
using eWebShop.WebUI.Contracts;
using eWebShop.WebUI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eWebShop.WebUI.BootStrapper
{
    public static class ScopeServicesConfigure
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(Repository<>));

            services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<CatalogViewModelService>();
            services.Configure<CatalogSettings>(Configuration);
            services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddMemoryCache();

            return services;
        }
    }
}
