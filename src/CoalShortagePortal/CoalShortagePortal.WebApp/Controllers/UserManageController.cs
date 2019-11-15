using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoalShortagePortal.Core;
using CoalShortagePortal.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoalShortagePortal.WebApp.Controllers
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class UserManageController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        public UserManageController(UserManager<IdentityUser> userManager, ILogger<UserManageController> logger)
        {
            // acquire user manager via dependency injection
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            UserListVM vm = new UserListVM();
            vm.Users = new List<UserListItemVM>();
            // get the list of users
            List<IdentityUser> users = await _userManager.Users.ToListAsync();
            foreach (IdentityUser user in users)
            {
                // get user is of admin role
                bool isAdmin = (await _userManager.GetRolesAsync(user)).Any(r => r == SecurityConstants.AdminRoleString);
                if (!isAdmin)
                {
                    // add user to vm only if not admin
                    vm.Users.Add(new UserListItemVM
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        Email = user.Email
                    });
                }

            }
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = vm.Username, Email = vm.Email };
                var result = await _userManager.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            UserCreateVM vm = new UserCreateVM()
            {
                Email = user.Email,
                Username = user.UserName
            };
            return View(vm);
        }

        // helper function
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}