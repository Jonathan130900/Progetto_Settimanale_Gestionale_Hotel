using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimanale_Gestionale_Hotel.Models;

namespace Progetto_Settimanale_Gestionale_Hotel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<ApplicationRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfermaPassword)
                {
                    ViewData["PasswordMismatch"] = "Le password non coincidono!";
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Nome + model.Cognome.Substring(0, 1),
                    Email = model.Email,
                    FullName = model.Nome + " " + model.Cognome
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(model.Role);
                    if (!roleExist)
                    {
                        ModelState.AddModelError("", "The specified role does not exist.");
                        return View(model);
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

    }
}
