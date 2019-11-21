using CoalShortagePortal.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CoalShortagePortal.WebApp.Models;
using System.Collections.Generic;
using CoalShortagePortal.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoalShortagePortal.Core.Entities;
using System;

namespace CoalShortagePortal.WebApp.Controllers
{
    [Authorize(Roles = SecurityConstants.AdminRoleString)]
    public class OtherReasonGens : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public OtherReasonGens(UserManager<IdentityUser> userManager, ILogger<UserManageController> logger, ApplicationDbContext dbContext)
        {
            // acquire user manager, db context via dependency injection
            _userManager = userManager;
            _logger = logger;
            _context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            GenCoalShortageListVM vm = new GenCoalShortageListVM
            {
                // get the list of generators
                Gens = await _context.GeneratingStationForOtherReasons.Select(g => new GenCoalShortageListItemVM()
                {
                    Id = g.Id,
                    StartDate = g.StartDate,
                    EndDate = g.EndDate,
                    Name = g.Name,
                    Location = g.Location,
                    Agency = g.Agency,
                    Capacity = g.Capacity,
                    UserId = g.User.Id,
                    UserName = g.User.UserName
                }).ToListAsync()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // get users info for combo box
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenCoalShortageCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                GeneratingStationForOtherReason gen = new GeneratingStationForOtherReason
                {
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    Name = vm.Name,
                    Location = vm.Location,
                    Agency = vm.Agency,
                    Capacity = vm.Capacity,
                    UserId = vm.UserId
                };
                _context.Add(gen);
                int x = await _context.SaveChangesAsync();

                if (x == 1)
                {
                    _logger.LogInformation("Generator for Other Reasons created");

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    string msg = $"Generator for Other Reasons not created as db returned num insertions as {x}";
                    _logger.LogInformation(msg);
                    //todo create custom exception
                    throw new Exception(msg);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GeneratingStationForOtherReason gen = await _context.GeneratingStationForOtherReasons.FindAsync(id);
            IdentityUser user = await _userManager.FindByIdAsync(gen.UserId);
            if (gen == null)
            {
                return NotFound();
            }

            //todo handle user not present as custom exception
            //todo handle start > end date as custom exception
            if (gen.StartDate > gen.EndDate)
            {
                throw new Exception("Start Date not be greater than end date");
            }

            GenCoalShortageCreateVM vm = new GenCoalShortageCreateVM()
            {
                StartDate = gen.StartDate,
                EndDate = gen.EndDate,
                Name = gen.Name,
                Location = gen.Location,
                Agency = gen.Agency,
                Capacity = gen.Capacity,
                UserId = gen.UserId
            };
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "UserName", gen.UserId);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GenCoalShortageCreateVM vm)
        {
            if (ModelState.IsValid)
            {
                GeneratingStationForOtherReason gen = await _context.GeneratingStationForOtherReasons.FindAsync(id);
                if (gen == null)
                {
                    return NotFound();
                }

                //todo handle start > end date as custom exception
                if (gen.StartDate > gen.EndDate)
                {
                    throw new Exception("Start Date not be greater than end date");
                }

                // update shift object as per user changes
                gen.StartDate = vm.StartDate;
                gen.EndDate = vm.EndDate;
                gen.Name = vm.Name;
                gen.Location = vm.Location;
                gen.Agency = vm.Agency;
                gen.Capacity = vm.Capacity;
                gen.UserId = vm.UserId;

                try
                {
                    _context.Update(gen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // check if the shift we are trying to edit was already deleted due to concurrency
                    if (!_context.GeneratingStationForOtherReasons.Any(g => g.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, redisplay form
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "UserName", vm.UserId);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            GeneratingStationForOtherReason gen = await _context.GeneratingStationForOtherReasons.FindAsync(id);
            if (gen == null)
            {
                return NotFound();
            }

            GenCoalShortageCreateVM vm = new GenCoalShortageCreateVM()
            {
                StartDate = gen.StartDate,
                EndDate = gen.EndDate,
                Name = gen.Name,
                Location = gen.Location,
                Agency = gen.Agency,
                Capacity = gen.Capacity,
                UserId = gen.UserId
            };
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            GeneratingStationForOtherReason gen = await _context.GeneratingStationForOtherReasons.FindAsync(id);
            if (gen == null)
            {
                return NotFound();
            }

            _context.GeneratingStationForOtherReasons.Remove(gen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}