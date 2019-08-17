using eWebShop.WebUI.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace eWebShop.WebUI.BootStrapper
{
    public static class HealthCheckConfig
    {
        public static IServiceCollection AddHealthCheckConfig(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<HomePageHealthCheck>("home_page_health_check")
                .AddCheck<ApiHealthCheck>("api_health_check");

            return services;
        }
    }
}
