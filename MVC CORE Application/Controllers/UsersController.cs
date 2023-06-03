using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Consts;
using System.Globalization;

namespace MVC_CORE_Application.Controllers
{
    [Authorize]
    public class UsersController : Controller 
    {

        public UserManager<ApplicationUser> UserManager { get; }

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }


        public async Task<IActionResult> Index(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var users = await UserManager.Users.ToListAsync();
                return View(users);

            }
            
            var searchedUser = await UserManager.FindByEmailAsync(searchValue);
            if (searchedUser != null) { 
                        return View( new List<ApplicationUser>() { searchedUser });

            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = await UserManager.FindByIdAsync(user.Id);
                updatedUser.UserName = user.UserName;
                updatedUser.PhoneNumber = user.PhoneNumber;
                updatedUser.NormalizedUserName = user.UserName.ToUpper();
                await UserManager.UpdateAsync(updatedUser);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authorize(Roles = Role.Admin)]

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var deletedUser = await UserManager.FindByIdAsync(user.Id);
            var isDeleted = await UserManager.DeleteAsync(deletedUser);
            if (isDeleted.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in isDeleted.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);

            }
            return View(user);
        }

    }
}
