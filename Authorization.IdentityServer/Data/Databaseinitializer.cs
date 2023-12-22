using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Authorization.IdentityServer.Data
{
    public static class Databaseinitializer
    {
        public static void Initialize(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>();

            var user = new IdentityUser
            {
                UserName = "User"
            };

            var result = userManager.CreateAsync(user, "123weqw").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }
        }
    }
}
