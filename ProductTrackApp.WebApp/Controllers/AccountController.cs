using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using ProductTrackApp.Entities;

namespace ProductTrackApp.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ValidateUserLoginRequest request, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                var httpClient = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var postResponse = await httpClient.PostAsync("http://localhost:7139/api/accounts/Login", content);
                var response = await postResponse.Content.ReadAsStringAsync();

                if (response.Contains("Invalid credentials"))
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                    return View();
                }

                JObject json = JObject.Parse(response);
                string jwtToken = json["token"].ToString();

                HttpContext.Session.SetString("JwtToken", jwtToken);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = new JwtSecurityToken(jwtToken);

                var jwtClaims = securityToken.Claims;

                foreach (var claim in jwtClaims)
                {
                    HttpContext.Session.SetString(claim.Type, claim.Value);
                }

                var userId = securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid).Value;
                var username = securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var role = securityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

                Claim[] claims = new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, userId),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return Redirect("/");

            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }


    }
}

