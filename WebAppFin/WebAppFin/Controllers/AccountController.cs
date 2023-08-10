using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Text.RegularExpressions;
using WebAppFin.Helper;
using WebAppFin.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _db;
		UserManager<ApplicationUser> _userManager;
		SignInManager<ApplicationUser> _signInManager;
		RoleManager<IdentityRole> _RoleManager;

		public AccountController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
		{
			_db = db;
			_userManager = userManager;
			_RoleManager = roleManager;
			_signInManager = signInManager;
		}

		public async Task<IActionResult> Login()
		{
            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                Name = "admin",
                Surname = "admin"
            };
            var r = await _userManager.CreateAsync(adminUser, "Admin_1234");
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login mlogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(mlogin.Email, mlogin.Password, mlogin.RememberMe, false);

                if (result.Succeeded)
                {
                    if (mlogin.Email == "admin@admin.com")
                    {
                        return RedirectToAction("Index", "Admin"); // Redirect to admin page
                    }
                    else
                    {
                        return RedirectToAction("Index", "ShoppingList");
                    }
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View(mlogin);
        }


        public async Task<IActionResult> Register()
		{
			if (!_RoleManager.RoleExistsAsync(Helper.admin).GetAwaiter().GetResult())
			{
				await _RoleManager.CreateAsync(new IdentityRole(Helper.admin));
				await _RoleManager.CreateAsync(new IdentityRole(Helper.client));
			}

			var adminUser = new ApplicationUser
			{
				UserName = "admin@admin.com",
				Email = "admin@admin.com",
				Name = "admin",
				Surname = "admin"
			};
			var r = await _userManager.CreateAsync(adminUser, "Admin_1234");

			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register mregister)
        {
            if (ModelState.IsValid)
            {
                // Check if the email already exists in the database
                var existingUser = await _userManager.FindByEmailAsync(mregister.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "This email is already registered.");
                    return View(mregister);
                }

                var user = new ApplicationUser
                {
                    UserName = mregister.Email,
                    Name = mregister.Name,
                    Surname = mregister.Surname,
                    Email = mregister.Email
                };

                var r = await _userManager.CreateAsync(user, mregister.Password);

                if (mregister.Email == "admin@admin.com")
                {
                    await _userManager.AddToRoleAsync(user, mregister.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Admin"); // Redirect to admin page
                }
                else
                {
                    if (r.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, mregister.RoleName);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "ShoppingList");
                    }
                    else
                    {
                        foreach (var error in r.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(mregister);
                    }
                }
            }
            return View(mregister);
        }


        [HttpPost]
        public async Task<IActionResult> Logoff() {
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}

    }
}
