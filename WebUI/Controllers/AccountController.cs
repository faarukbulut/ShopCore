using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Repositories.Abstract;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailRepository _mailRepository;
        private readonly ICartService _cartService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailRepository mailRepository, ICartService cartService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailRepository = mailRepository;
            _cartService = cartService;
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
                _cartService.Initialize(user.Id);

                //var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackurl = Url.Action("ConfirmEmail", "Account", new { userID = user.Id, token = tokenCode }, HttpContext.Request.Scheme);

                //_mailRepository.MailGonder(
                //    p.FullName,
                //    p.Email,
                //    $"Hesabınızı doğrulamak için <a href='{callbackurl}'>buraya tıklayınız.</a>",
                //    "Hesap Doğrulama"
                //);

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
                //if (!await _userManager.IsEmailConfirmedAsync(user))
                //{
                //    ModelState.AddModelError("", "Lütfen hesabınızı mail ile onaylayınız.");
                //    return View(p);
                //}

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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if(user == null)
            {
                return View();
            }

            var tokenCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackurl = Url.Action("ResetPassword", "Account", new { userID = user.Id, token = tokenCode }, HttpContext.Request.Scheme);

            _mailRepository.MailGonder(
                user.FullName,
                Email,
                $"Şifrenizi yenilemek için <a href='{callbackurl}'>buraya tıklayınız.</a>",
                "Şifre Sıfırlama"
            );

            return RedirectToAction("Login", "Account");
        }

        public IActionResult ResetPassword(string userID, string token)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Default");
            }

            var model = new ResetPasswordModel { Token = token, UserID = userID};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel p)
        {
            var user = await _userManager.FindByIdAsync(p.UserID);

            if(user == null)
            {
                return RedirectToAction("Index", "Default");
            }

            var result = await _userManager.ResetPasswordAsync(user, p.Token, p.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(p);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
