using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using RoleAndManagement.Models;
using RoleAndManagement.Service;
using RoleAndManagement.ViewModels;

namespace RoleAndManagement.Controllers
{
    // [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }



        //login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            if(ModelState.IsValid)
            {
                // 创建认证Cookie
                var user = await _userService.GetByUsername(model.Username);
                if (user != null && _userService.VerifyPassword(model.password, user.PasswordHash))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    // 添加角色声明
                    foreach (var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticatioinDefaults.AuthenticationScheme);

                    var authProperties = new AuthticationProperties
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTimeOffest.UtcNow.AddDays(7)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticatioinDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    if(Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User name or Password not correct")
                    return View(model);
                }
            }

            return View(model);
        }

        //register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password not match");
                    return View(model);
                }

                if (await _userService.GetByUsername(model.Username) != null)
                {
                    ModelState.AddModelError(string.Empty, "User name alreday exist")
                    return View(model);
                }

                if (await _userService.GetByUsername(model.Email) != null)
                {
                    ModelState.AddModelError(string.Empty, "Email alreday exist")
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Roles = new[] { "User" }
                }

                bool result = _userService.Create(user, model.Password);
                if (result)
                {
                    TempData["SuccessMessage"] = " Create Account Successful";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registration fail");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticatioinDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}