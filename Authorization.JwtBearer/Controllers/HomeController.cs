using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.JwtBearer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate() 
        {
            // find user
            // find INN 

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Arcadiy"),
                new Claim(JwtRegisteredClaimNames.Email, "@arc@mail.com")
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.Constants.SecretKey);

            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Constants.Issuer,
                Constants.Constants.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);



            ViewBag.Token = value;
            return View();
        }
    }
}
