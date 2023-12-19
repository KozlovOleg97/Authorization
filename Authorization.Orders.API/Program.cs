using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Authorization.Orders.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            /// <summary>
            /// Default value for AuthenticationScheme property in the <see cref="JwtBearerOptions"/>.
            /// </summary>
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.Authority = "http://localhost:10000";
                config.Audience = "OrdersAPI";
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
