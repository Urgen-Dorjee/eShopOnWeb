using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eWebShop.Persistence.Auth
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext context, UserManager<ApplicationUser> _userManager, ILogger<AppIdentityDbContextSeed> logger, int? retry = 0)
        {
            var ensure = context.Database.EnsureCreated();
            if (ensure.Equals(true))
            {
                int retryForAvailability = retry.Value;               
                if (!context.Users.Any())
                {
                    try
                    {
                        var defaultUser = new ApplicationUser { UserName = "urgen0240@gmail.com", Email = "urgen0240@gmail.com" };
                        await _userManager.CreateAsync(defaultUser, "Pass@word1");
                    }
                    catch (Exception ex)
                    {
                        if (retryForAvailability < 5)
                        {
                            retryForAvailability++;
                            logger.LogError(ex.Message);
                            await SeedAsync(context, _userManager, logger, retryForAvailability);
                        }
                    }
                }              
            }           
        }
    }
}
