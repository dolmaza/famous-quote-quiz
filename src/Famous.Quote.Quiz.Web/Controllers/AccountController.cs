using Famous.Quote.Quiz.Application.Services;
using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Web.Infrastructure;
using Famous.Quote.Quiz.Web.Infrastructure.Identity;
using Famous.Quote.Quiz.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("login", Name = RouteNames.Account.Login)]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginEditorModel { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginEditorModel model)
        {
            var user = await _userService.GetAuthorizedUserAsync(model.UserName, model.Password);
            if (user == null || user.Status == UserStatus.Disabled)
            {
                model.ErrorMessage = "UserName or password is not correct";
                return View(model);
            }
            else
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                var identity = IdentityConfig.CreateClaimsIdentity(user);

                await HttpContext.SignInAsync
                (
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(1) }
                );

                return Redirect(string.IsNullOrEmpty(model.ReturnUrl) ? Url.RouteUrl(RouteNames.Home.HomePage) : model.ReturnUrl);
            }
        }

        [HttpGet]
        [Route("logout", Name = RouteNames.Account.Logout)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute(RouteNames.Account.Login);
        }
    }
}
