using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TournamentPlanner.DAL.Entities;
using TournamentPlanner.Models;

namespace TournamentPlanner.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailOptions _emailOptions;
        

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IOptions<TournamentPlanner.EmailOptions> options)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
            _emailOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmailProcess(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code },
                protocol: HttpContext.Request.Scheme);
            var test = _emailOptions.Address;
            EmailService emailService = new EmailService(
                _emailOptions.Address,
                _emailOptions.SenderName, 
                _emailOptions.Smtp, 
                _emailOptions.Port, 
                _emailOptions.Password);

            await emailService.SendEmailAsync(email, "Confirm your account",
                $"Please confirm registration by: <a href='{callbackUrl}'>link</a>");
            return Content("For complete registration please go to your e-mail and click link");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await ConfirmEmailProcess(model.Email);
                    return Content("For complete registration please go to your e-mail and click link");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(Register));
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.isConfirm = true;
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                ViewBag.isConfirm = true;

                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You didn't confirm e-mail");
                        ViewBag.isConfirm = false;
                        return View(model);
                    }
                }
                //Третий параметр метода указывает, надо ли сохранять устанавливаемые куки на долгое время
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                // set cookies
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "guest");
                return RedirectToAction("Index", "Home");
            }
            else
                return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // remove cooky authnetification
            await _signInManager.SignOutAsync();
            return RedirectToAction("Register", "Account");
        }
    }
}
