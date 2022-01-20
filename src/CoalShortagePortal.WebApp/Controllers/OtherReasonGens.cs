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
using CoalShortagePortal.WebApp.Extensions;
using CoalShortagePortal.WebApp.Utils;

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
            GenCoalShortageListVM vm = new()
            {
                // get the list of generators
                Gens = await _context.GeneratingStationForOtherReasons.OrderBy(x => x.SerialNum).Select(g => new GenCoalShortageListItemVM()
                {
                    Id = g.Id,
                    StartDate = g.StartDate,
                    EndDate = g.EndDate,
                    SerialNum = g.SerialNum,
                    Name = g.Name,
                    Location = g.Location,
                    Agency = g.Agency,
                    Capacity = g.Capacity,
                    UserId = g.User.Id,
                    UserName = g.User.UserName,
                    Region = g.Region.ToString()
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
                // handle start > end date
                if (vm.StartDate > vm.EndDate)
                {
                    //todo use custom exception for start > end date
                    throw new Exception("Start Date not be greater than end date");
                }

                // perform overlap check for the generator name before insertion
                if (await CheckIfOverlapExists(vm.StartDate, vm.Name))
                {
                    // todo use custom exception for this
                    throw new Exception($"An overlapping entry exists for {vm.Name}, hence we are unable to create this generator for these {vm.StartDate:dd-MMM-yyyy} and {vm.EndDate:dd-MMM-yyyy} dates");
                }

                GeneratingStationForOtherReason gen = new()
                {
                    StartDate = vm.StartDate,
                    EndDate = vm.EndDate,
                    SerialNum = vm.SerialNum,
                    Name = TextUtils.SanitizeText(vm.Name),
                    Location = TextUtils.SanitizeText(vm.Location),
                    Agency = TextUtils.SanitizeText(vm.Agency),
                    Capacity = vm.Capacity,
                    Region = vm.Region,
                    UserId = vm.UserId
                };
                _context.Add(gen);
                int numInserted = await _context.SaveChangesAsync();

                if (numInserted == 1)
                {
                    _logger.LogInformation("Generator for Other Reasons created");

                    return RedirectToAction(nameof(Index)).WithSuccess("Generator for Other Reasons created");
                }
                else
                {
                    string msg = $"Generator for Other Reasons not created as db returned num insertions as {numInserted}";
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
            //IdentityUser user = await _userManager.FindByIdAsync(gen.UserId);
            if (gen == null)
            {
                return NotFound();
            }

            GenCoalShortageCreateVM vm = new()
            {
                StartDate = gen.StartDate,
                EndDate = gen.EndDate,
                SerialNum = gen.SerialNum,
                Name = gen.Name,
                Location = gen.Location,
                Agency = gen.Agency,
                Region = gen.Region,
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

                // handle start > end date
                if (vm.StartDate > vm.EndDate)
                {
                    //todo use custom exception for start > end date
                    throw new Exception("Start Date not be greater than end date");
                }

                // perform overlap check only if name or startdate changes
                if (vm.StartDate != gen.StartDate || vm.Name != gen.Name)
                {
                    // perform overlap check for the generator name before editing
                    if (await CheckIfOverlapExists(vm.StartDate, vm.Name, id))
                    {
                        // todo use custom exception for this
                        throw new Exception($"An overlapping entry exists for {vm.Name}, hence we are unable to edit this generator for these {vm.StartDate:dd-MMM-yyyy} and {vm.EndDate:dd-MMM-yyyy} dates");
                    }
                }

                // update object as per user changes
                gen.StartDate = vm.StartDate;
                gen.EndDate = vm.EndDate;
                gen.SerialNum = vm.SerialNum;
                gen.Name = TextUtils.SanitizeText(vm.Name);
                gen.Location = TextUtils.SanitizeText(vm.Location);
                gen.Agency = TextUtils.SanitizeText(vm.Agency);
                gen.Capacity = vm.Capacity;
                gen.UserId = vm.UserId;
                gen.Region = vm.Region;

                try
                {
                    _context.Update(gen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // check if the we are trying to edit was already deleted due to concurrency
                    if (!_context.GeneratingStationForOtherReasons.Any(g => g.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)).WithSuccess("Generator for Other Reasons Edit done");
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

            GenCoalShortageCreateVM vm = new()
            {
                StartDate = gen.StartDate,
                EndDate = gen.EndDate,
                SerialNum = gen.SerialNum,
                Name = gen.Name,
                Location = gen.Location,
                Agency = gen.Agency,
                Capacity = gen.Capacity,
                UserId = gen.UserId,
                Region = gen.Region
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
            return RedirectToAction(nameof(Index)).WithSuccess("Generator for Other Reasons deleted");
        }

        private async Task<bool> CheckIfOverlapExists(DateTime StartDate, string stationName, int exclusionId = -1)
        {
            // perform overlap check for the generator name before insertion
            bool overlapEntryExists = await _context.GeneratingStationForOtherReasons.AnyAsync(x => (x.Id != exclusionId) && (x.StartDate <= StartDate) && (x.EndDate >= StartDate) && (x.Name == stationName));

            return overlapEntryExists;
        }
    }
}