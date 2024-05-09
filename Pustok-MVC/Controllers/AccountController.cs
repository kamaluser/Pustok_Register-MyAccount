using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok_MVC.Models;
using Pustok_MVC.ViewModels;

namespace Pustok_MVC.Controllers
{
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
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Register(MemberRegisterViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var existUser = await _userManager.FindByEmailAsync(model.Email);
			if (existUser != null)
			{
				ModelState.AddModelError("", "This email is already registered.");
				return View(model);
			}

			AppUser user = new AppUser
			{
				UserName = model.UserName,
				Email = model.Email,
				Fullname = model.FullName
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View(model);
			}

			await _signInManager.SignInAsync(user, true);

			return RedirectToAction("profile","account");
		}


		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(MemberLoginModel model, string? returnUrl)
		{
			if ((!ModelState.IsValid)) return View();

			AppUser? user = await _userManager.FindByNameAsync(model.UserName);

			if (user == null)
			{
				ModelState.AddModelError("","UserName or Password is incorrect!");
				return View();
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

			if (result.IsLockedOut)
			{
				ModelState.AddModelError("", "You are locked out for 5 minutes!");
				return View();
			}

            else if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password incorrect");
                return View();
            }

			return returnUrl!=null ? Redirect(returnUrl) : RedirectToAction("index","home");

		}


		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("login", "account");
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("login", "account");
            }
            return View(user);
        }

    }
}
