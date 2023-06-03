using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Collections.Generic;
using MVC_CORE_Application.Consts;
using MVC_CORE.DDL.Entities;
using MVC_CORE_Application.Consts;
using MVC_CORE_Application.Models.ViewModel;

    using Newtonsoft.Json;

namespace Demo.Pl.Controllers
{
    [Authorize(Roles =Role.Admin)]
    public class RolesController : Controller
    {
        public RolesController(UserManager<ApplicationUser> user, RoleManager<IdentityRole> roles)
        {
            _user = user;
            _roles = roles;
        }

        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _roles;


        public async Task<IActionResult> addRoles(string userId)
        {
            var user = await _user.FindByIdAsync(userId);
            var userRoles = await _user.GetRolesAsync(user);

            var allRoles = await _roles.Roles.ToListAsync();
            if (allRoles != null)
            {
                var roleList = allRoles.Select(r => new RoleViewModel()
                {
                    roleId = r.Id,
                    roleName = r.Name,
                    useRole = userRoles.Any(x => x == r.Name)
                });
                ViewBag.userName = user.UserName;
                ViewBag.userId = userId;
                return View(roleList);
            }
            else
                return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addRoles(string userId, string jsonRoles)
        {
            var user = await _user.FindByIdAsync(userId);
            List<RoleViewModel> myRoles =
                JsonConvert.DeserializeObject<List<RoleViewModel>>(jsonRoles);

            if (user != null)
            {
                var userRoles = await _user.GetRolesAsync(user);

                foreach (var role in myRoles)
                {
                    if (userRoles.Any(x => x == role.roleName.Trim()) && !role.useRole)
                    {
                        await _user.RemoveFromRoleAsync(user, role.roleName.Trim());
                    }

                    if (!userRoles.Any(x => x == role.roleName.Trim()) && role.useRole)
                    {
                        await _user.AddToRoleAsync(user, role.roleName.Trim());
                    }
                }

                return RedirectToAction(nameof(Index),"Users");
            }
            else
                return NotFound();
        }
    }
}