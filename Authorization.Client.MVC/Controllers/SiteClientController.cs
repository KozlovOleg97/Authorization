using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Client.MVC.Controllers
{
    [Route("[controller]")]
    public class SiteClientController : Controller
    {
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("[action]")]
        public IActionResult Secret()
        {
            return View();
        }
    }
}
