using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace eWebShop.WebUI.HealthChecks
{
    public class ApiHealthCheck : IHealthCheck
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _linkGenerator;

        public ApiHealthCheck(IHttpContextAccessor accessor, LinkGenerator linkGenerator)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
            _linkGenerator = linkGenerator ?? throw new ArgumentNullException(nameof(linkGenerator));
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var request = _accessor.HttpContext.Request;
            var apiLink = _linkGenerator.GetPathByAction("List", "Catalog");
            var myUrl = $"{request.Scheme}://{request.Host.ToString()}{apiLink}";
            var client = new HttpClient();
            var response = await client.GetAsync(myUrl);
            var pageContents = await response.Content.ReadAsStringAsync();
            if (pageContents.Contains(".NET Bot Black Sweatshirt"))
            {
                return HealthCheckResult.Healthy("The check indicates a healthy result");
            }
            return HealthCheckResult.Unhealthy("The check indicates an unhealthy result");
        }
    }
}
