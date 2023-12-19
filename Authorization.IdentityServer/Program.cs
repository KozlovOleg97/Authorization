namespace Authorization.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityServer()
                .AddInMemoryClients(Configuration.GetClients())
                .AddInMemoryApiResources(Configuration.GetApiResources())
                .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
                .AddDeveloperSigningCredential();  

            builder.Services.AddControllersWithViews();

            var app = builder.Build();



            app.UseIdentityServer();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
