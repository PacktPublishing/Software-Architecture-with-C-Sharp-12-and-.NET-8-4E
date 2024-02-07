using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PackagesManagement.Models;
using PackagesManagement.Models.Account;

namespace PackagesManagement.Controllers
{
    public class AccountController(
            UserManager<IdentityUser<int>> _userManager,
            SignInManager<IdentityUser<int>> _signInManager) : Controller
    {
        
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            // Clear the existing external cookie 
            //to ensure a clean login process
            await HttpContext
                .SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
           string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (User?.Identity?.IsAuthenticated??false)
            {
                await HttpContext.SignOutAsync();
            }
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName, 
                    model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "wrong user name or password");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
