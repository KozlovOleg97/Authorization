using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace Authorization.JwtBearer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);

                    var key = new SymmetricSecurityKey(secretBytes);


                    // request through query, to get 200 (OK)
                    config.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("t"))
                            {
                                context.Token = context.Request.Query["t"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                        
                 

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Constants.Constants.Issuer,
                        ValidAudience = Constants.Constants.Audience,
                        IssuerSigningKey = key
                    }; 
                });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
