using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Users.API.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SiteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

#pragma warning disable ASP0023 // Route conflict detected between controller actions
        [Route("[action]")]
#pragma warning restore ASP0023 // Route conflict detected between controller actions

        public IActionResult Index()
        {
            return View();
        }


#pragma warning disable ASP0023 // Route conflict detected between controller actions
        [Route("[action]")]
#pragma warning restore ASP0023 // Route conflict detected between controller actions
        public async Task<IActionResult> GetOrders()
        {
            // RETRIEVE TO IDENTITYSERVER

            var authClient = _httpClientFactory.CreateClient();
            var disvoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");

            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disvoveryDocument.TokenEndpoint,

                    ClientId = "client_id",
                    ClientSecret = "client_secret",

                    Scope = "OrdersAPI"
                });

            // RETRIEVE TO ORDERS
            var ordersClient = _httpClientFactory.CreateClient();


            ordersClient.SetBearerToken(tokenResponse.AccessToken);


            var response = await ordersClient.GetAsync("https://localhost:5001/site/secret");


            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = response.StatusCode.ToString();
                return View();
            }

            var message = await response.Content.ReadAsStringAsync();

            ViewBag.Message = message;

            return View();
        }
    }
}
