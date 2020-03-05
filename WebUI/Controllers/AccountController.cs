using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Identity;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AutoValidateAntiforgeryToken]//Token değerine sahip olup olmadağına bakar get metodu hariç bütün metotlar kontrol edilir
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Mail,
                FullName = registerModel.FullName
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("", "Bilinmeyen bir hata oluştu lütfen tekrar deneyiniz");
            return View(registerModel);
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = null)
        {
            returnUrl = returnUrl ?? "~/";
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }
            var user = await _userManager.FindByEmailAsync(loginModel.Mail);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu mail adresi ile daha önce hesap oluşturulmamış");
                return View(loginModel);
            }
           
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            ModelState.AddModelError("", "Mail adresi veya parola yanlış");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

    }
}