using Authorization.FacebookDemo.Data;
using Authorization.FacebookDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Authorization.FacebookDemo
{
    public class Program
    {
      
        
        public static void Main(string[] args)
        {
           
        var builder = WebApplication.CreateBuilder(args);

            
            
            builder.Services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseInMemoryDatabase("MEMORY");
            })
                .AddIdentity<ApplicationUser, ApplicationRole>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            

            builder.Services.AddAuthentication()
                .AddFacebook(config =>
                {
                    //config.AppId = _configuration["Authentication:Facebook:AppId"];
                    //config.AppSecret = _configuration["Authentication:Facebook:AppSecret"]; 
                });
            

            // Doesn't work with Identity4

            //builder.Services.AddAuthentication("Cookie")
            //.AddCookie("Cookie", config =>
            //{
            //    config.LoginPath = "/Admin/Login";
            //    config.AccessDeniedPath = "/Home/AccessDenied";
            //});

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Admin/Login";
                config.AccessDeniedPath = "/Home/AccessDenied";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                options.AddPolicy("Manager", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
                        || x.User.HasClaim(ClaimTypes.Role, "Administrator"));



                });
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                Databaseinitializer.Init(scope.ServiceProvider);
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
