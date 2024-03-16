using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel p)
        {
            var user = new AppUser
            {
                UserName = p.Username,
                Email = p.Email,
                FullName = p.FullName
            };

            var result = await _userManager.CreateAsync(user, p.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Kayıt sırasında hata oluştu. Lütfen tekrar deneyiniz.");
            return View(p);
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel p)
        {
            var user = await _userManager.FindByNameAsync(p.Username);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            ModelState.AddModelError("", "Giriş işlemi başarısız");
            return View(p);
        }

    }
}
