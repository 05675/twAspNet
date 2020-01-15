using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twAspnet.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;

        public AuthController(IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            this.authenticationSchemeProvider = authenticationSchemeProvider;
        }
        public async Task<IActionResult> Login()
        {
            var allSchemeProvider = (await authenticationSchemeProvider.GetAllSchemesAsync()).Select(n => n.DisplayName).Where(n => !string.IsNullOrEmpty(n));
            return View(allSchemeProvider);
        }
        public async Task<IActionResult> Login(string ReturnUrl)
        {
            var allSchemeProvider = (await authenticationSchemeProvider.GetAllSchemesAsync()).Select(n => n.DisplayName).Where(n => !string.IsNullOrEmpty(n));
            return View(allSchemeProvider);
        }
        public IActionResult SignIn(string provider)
        {
            Request.Scheme = "https";
            Request.Host = new HostString("tweetapi.work");
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
    }
}
