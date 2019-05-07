using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Librum.Helpers;
using Librum.Models;
using Librum.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Librum.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var usersStore = Configuration.GetSection("UsersStore").Get<UserStoreItem[]>().ToList();
                
                if(usersStore.Any(x => x.Email.Equals(loginViewModel.Email)))
                {
                    var user = usersStore.First(x => x.Email.Equals(loginViewModel.Email));
                    if(CryptoHelper.Sha1(loginViewModel.Password.ToLower()).Equals(user.Password.ToLower()))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginViewModel.Email),
                            new Claim(ClaimTypes.Role, user.Role),
                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
                        {
                            IsPersistent = true
                        });
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "The password is incorrect.");
                    }
                }
                ModelState.AddModelError("Email", "This user does not exists.");
            }
            return View(loginViewModel);
        }

        [Authorize]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}