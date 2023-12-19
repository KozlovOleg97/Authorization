using Microsoft.AspNetCore.Authentication.Cookies;

namespace Authorization.Client.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = "oidc";
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect("oidc", config =>
                {
                    config.Authority = "https://localhost:2001";
                    config.ClientId = "client_id_mvc";
                    config.ClientSecret = "client_secret_mvc";
                    config.SaveTokens = true;

                    config.ResponseType = "code";
                });

            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
