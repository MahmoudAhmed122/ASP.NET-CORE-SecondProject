using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Consts;
using MVC_CORE_Application.Helpers;
using MVC_CORE_Application.Models.ViewModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MVC_CORE_Application.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var password = await UserManager.CheckPasswordAsync(user, model.Password);
                    if (password)
                    {
                        var isSignIn = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (isSignIn.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await UserManager.FindByEmailAsync(model.Email);
                if(User != null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email.Split('@')[0],
                        Email = model.Email,
                        IsAgree = model.IsAgree
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                     await UserManager.AddToRoleAsync(user, Role.Admin);
                    return RedirectToAction(nameof(Login));

                    }
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                ModelState.AddModelError(string.Empty, "Email is exist !");

            }
            return View(model);

        }

        #endregion

        #region Forget Passowrd
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        Title = "Reset Passwrod!",
                        To = model.Email,
                        Body = resetPasswordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CompleteForgetPassword));

                }
                ModelState.AddModelError(string.Empty, "Email not Exist!");
            }
            return View(model);
        }




        public IActionResult CompleteForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {  

            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {

            return View();
        }

    }


}
