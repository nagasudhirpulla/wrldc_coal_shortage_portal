using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoalShortagePortal.Core.Entities;
using CoalShortagePortal.WebApp.Models;
using CoalShortagePortal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CoalShortagePortal.Core;

namespace CoalShortagePortal.WebApp.Controllers
{
    [Authorize]
    public class CoalDataController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public CoalDataController(UserManager<IdentityUser> userManager, ILogger<UserManageController> logger, ApplicationDbContext dbContext)
        {
            // acquire user manager, db context via dependency injection
            _userManager = userManager;
            _logger = logger;
            _context = dbContext;
        }

        private Task<IdentityUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> Index([FromQuery]DateTime? RecordDate)
        {
            DateTime entryDate;
            if (RecordDate.HasValue)
            {
                entryDate = RecordDate.Value.Date;
            }
            else
            {
                entryDate = DateTime.Now.Date;
            }
            // get the current logged in user
            IdentityUser usr = await GetCurrentUserAsync();
            bool usrIsAdmin = (await _userManager.GetRolesAsync(usr)).Any(r => r == SecurityConstants.AdminRoleString);
            GenResponseVM vm = await PopulateVMForUserDate(usr.Id, usrIsAdmin, entryDate);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Populate([Bind("RecordDate")]GenResponseVM model)
        {
            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GenResponseVM model)
        {
            // save the model data to db

            return RedirectToAction(nameof(Index), model);
        }

        //helper function
        private async Task<GenResponseVM> PopulateVMForUserDate(string userId, bool usrIsAdmin, DateTime entryDate)
        {
            // generate view model for the user response based on the entryDate
            GenResponseVM vm = new GenResponseVM() { RecordDate = entryDate };

            // get the coal shortage generators that user is assigned
            List<GeneratingStationForCoalShortage> coalShortageGens = await _context.GeneratingStationForCoalShortages
                                                            .Where(g => (g.StartDate <= entryDate) &&
                                                            (g.EndDate >= entryDate) &&
                                                            ((g.UserId == userId) || usrIsAdmin)).ToListAsync();
            // get the other reason generators that user is assigned
            List<GeneratingStationForOtherReason> otherReasonGens = await _context.GeneratingStationForOtherReasons
                                                            .Where(g => (g.StartDate <= entryDate) &&
                                                            (g.EndDate >= entryDate) &&
                                                            ((g.UserId == userId) || usrIsAdmin)).ToListAsync();
            // get the critical coal generators that user is assigned
            List<GeneratingStationForCriticalCoal> criticalCoalGens = await _context.GeneratingStationForCriticalCoals
                                                            .Where(g => (g.StartDate <= entryDate) &&
                                                            (g.EndDate >= entryDate) &&
                                                            ((g.UserId == userId) || usrIsAdmin)).ToListAsync();

            // add coal shortage gens
            foreach (GeneratingStationForCoalShortage gen in coalShortageGens)
            {
                vm.CoalShortageResponses.Add(new CoalShortageResponse()
                {
                    Station = gen.Name,
                    Location = gen.Location,
                    Agency = gen.Agency,
                    Capacity = gen.Capacity,
                    Remarks = ""
                });
            }

            // add other reasons gens
            foreach (GeneratingStationForOtherReason gen in otherReasonGens)
            {
                vm.OtherReasonsResponses.Add(new OtherReasonsResponse()
                {
                    Station = gen.Name,
                    Location = gen.Location,
                    Agency = gen.Agency,
                    Capacity = gen.Capacity,
                    Remarks = ""
                });
            }

            // add critical coal gens
            foreach (GeneratingStationForCriticalCoal gen in criticalCoalGens)
            {
                vm.CriticalCoalResponses.Add(new CriticalCoalResponse()
                {
                    Station = gen.Name,
                    Owner = gen.Owner,
                    Capacity = gen.Capacity,
                    Remarks = ""
                });
            }

            return vm;
        }
    }
}