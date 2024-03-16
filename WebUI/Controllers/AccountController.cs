using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Repositories.Abstract;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailRepository _mailRepository;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailRepository mailRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailRepository = mailRepository;
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
                var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackurl = Url.Action("ConfirmEmail", "Account", new { userID = user.Id, token = tokenCode }, HttpContext.Request.Scheme);

                _mailRepository.MailDogrulamaMailGonder(p.FullName, p.Email, callbackurl);

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

            if(user == null)
            {
                ModelState.AddModelError("", "Giriş işlemi başarısız");
                return View(p);
            }
            else
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "Lütfen hesabınızı mail ile onaylayınız.");
                    return View(p);
                }

                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemi başarısız");
                    return View(p);
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Default");
        }

        public async Task<IActionResult> ConfirmEmail(string userID, string token)
        {
            if(string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(token))
            {
                TempData["message"] = "Geçersiz kimlik bilgisi";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userID);

            if(user == null)
            {
                TempData["message"] = "Geçersiz kimlik bilgisi";
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }

            return View();
        }


    }
}
