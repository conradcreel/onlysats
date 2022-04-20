using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlysats.web.Models;
using System.Net.Http;
using onlysats.domain.Services;
using onlysats.web.Models.Auth;
using onlysats.domain.Services.Request.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace onlysats.web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService authService,
                                ILogger<AuthController> logger)
        {
            _logger = logger;

            _AuthService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model, [FromQuery] string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _AuthService.Login(new LoginRequest
                {
                    Username = model.Username,
                    Password = model.Password
                }).ConfigureAwait(continueOnCapturedContext: false);

                if (!loginResponse.ResponseDetails.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials");

                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim("Id", loginResponse.UserAccountId.ToString()),
                    new Claim("Role", loginResponse.Role.ToString()),
                    new Claim("ChatAccessToken", loginResponse.ChatAccessToken),
                    new Claim("Username", loginResponse.Username)
                };

                var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
