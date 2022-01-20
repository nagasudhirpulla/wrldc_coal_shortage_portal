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
using CoalShortagePortal.WebApp.Extensions;
using System.Text.RegularExpressions;
using CoalShortagePortal.WebApp.Utils;

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

        public async Task<IActionResult> Index([FromQuery] DateTime? RecordDate)
        {
            DateTime entryDate;
            if (RecordDate.HasValue)
            {
                entryDate = RecordDate.Value.Date;
            }
            else
            {
                entryDate = DateTime.Now.Date.AddDays(-1);
            }
            // get the current logged in user
            IdentityUser usr = await GetCurrentUserAsync();
            bool usrIsAdmin = (await _userManager.GetRolesAsync(usr)).Any(r => r == SecurityConstants.AdminRoleString);
            GenResponseVM vm = await PopulateVMForUserDate(usr.Id, usrIsAdmin, entryDate);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Populate([Bind("RecordDate")] GenResponseVM model)
        {
            return RedirectToAction(nameof(Index), new { model.RecordDate });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(GenResponseVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // save the model data to db
            // save CoalShortageResponses
            foreach (CoalShortageResponse resp in model.CoalShortageResponses)
            {
                // input sanitization
                CoalShortageResponse sanitResp = Sanitize(resp);
                // check if resp is to be added or inserted
                if (sanitResp.Id != 0)
                {
                    _context.Update(sanitResp);
                }
                else
                {
                    sanitResp.DataDate = model.RecordDate;
                    _context.CoalShortageResponses.Add(sanitResp);
                }
                await _context.SaveChangesAsync();
            }
            // save OtherReasonsResponses
            foreach (OtherReasonsResponse resp in model.OtherReasonsResponses)
            {
                OtherReasonsResponse sanitResp = Sanitize(resp);
                // check if resp is to be added or inserted
                if (sanitResp.Id != 0)
                {
                    _context.Update(sanitResp);
                }
                else
                {
                    sanitResp.DataDate = model.RecordDate;
                    _context.OtherReasonsResponses.Add(sanitResp);
                }
                await _context.SaveChangesAsync();
            }
            // save CriticalCoalResponses
            foreach (CriticalCoalResponse resp in model.CriticalCoalResponses)
            {
                CriticalCoalResponse sanitResp = Sanitize(resp);
                // check if resp is to be added or inserted
                if (sanitResp.Id != 0)
                {
                    _context.Update(sanitResp);
                }
                else
                {
                    sanitResp.DataDate = model.RecordDate;
                    _context.CriticalCoalResponses.Add(sanitResp);
                }
                await _context.SaveChangesAsync();
            }
            // redirect to the same page
            return RedirectToAction(nameof(Index), new { model.RecordDate }).WithSuccess("Coal data saved");
        }

        private static CoalShortageResponse Sanitize(CoalShortageResponse resp)
        {
            resp.Station = TextUtils.SanitizeText(resp.Station);
            resp.Location = TextUtils.SanitizeText(resp.Location);
            resp.Agency = TextUtils.SanitizeText(resp.Agency);
            resp.Remarks = TextUtils.SanitizeText(resp.Remarks);
            return resp;
        }

        private static OtherReasonsResponse Sanitize(OtherReasonsResponse resp)
        {
            resp.Station = TextUtils.SanitizeText(resp.Station);
            resp.Location = TextUtils.SanitizeText(resp.Location);
            resp.Agency = TextUtils.SanitizeText(resp.Agency);
            resp.Remarks = TextUtils.SanitizeText(resp.Remarks);
            return resp;
        }

        private static CriticalCoalResponse Sanitize(CriticalCoalResponse resp)
        {
            resp.Station = TextUtils.SanitizeText(resp.Station);
            resp.Owner = TextUtils.SanitizeText(resp.Owner);
            resp.Remarks = TextUtils.SanitizeText(resp.Remarks);
            return resp;
        }

        //helper function
        private async Task<GenResponseVM> PopulateVMForUserDate(string userId, bool usrIsAdmin, DateTime entryDate)
        {
            // generate view model for the user response based on the entryDate
            GenResponseVM vm = new() { RecordDate = entryDate };

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

            // fetch existing OtherReasonsResponses and initialize placeholder responses if any gens are assigned to the user
            if (coalShortageGens.Count > 0)
            {
                List<CoalShortageResponse> existingResponses = await _context.CoalShortageResponses.Where(r => r.DataDate == entryDate).ToListAsync();
                List<CoalShortageResponse> yestResponses = await _context.CoalShortageResponses.Where(r => r.DataDate == entryDate.AddDays(-1)).ToListAsync();
                // Add critical coal gen responses to VM
                foreach (GeneratingStationForCoalShortage gen in coalShortageGens)
                {
                    bool respExists = false;
                    bool yestRespExists = false;
                    foreach (CoalShortageResponse resp in existingResponses.Where(r => r.Station == gen.Name))
                    {
                        resp.SerialNum = gen.SerialNum;
                        vm.CoalShortageResponses.Add(resp);
                        respExists = true;
                    }
                    if (usrIsAdmin && !respExists)
                    {
                        // populate yesterday data only if user is in admin role
                        foreach (CoalShortageResponse resp in yestResponses.Where(r => r.Station == gen.Name))
                        {
                            resp.SerialNum = gen.SerialNum;
                            resp.Id = 0;
                            vm.CoalShortageResponses.Add(resp);
                            yestRespExists = true;
                        }
                    }
                    if (!respExists && !yestRespExists)
                    {
                        // If there is no existing response for gen, then add a new response
                        vm.CoalShortageResponses.Add(new CoalShortageResponse()
                        {
                            DataDate = entryDate,
                            SerialNum = gen.SerialNum,
                            Station = gen.Name,
                            Location = gen.Location,
                            Agency = gen.Agency,
                            Capacity = gen.Capacity,
                            Remarks = ""
                        });
                    }

                }
            }

            // fetch existing OtherReasonsResponses and initialize placeholder responses if any gens are assigned to the user
            if (otherReasonGens.Count > 0)
            {
                List<OtherReasonsResponse> existingResponses = await _context.OtherReasonsResponses.Where(r => r.DataDate == entryDate).ToListAsync();
                List<OtherReasonsResponse> yestResponses = await _context.OtherReasonsResponses.Where(r => r.DataDate == entryDate.AddDays(-1)).ToListAsync();
                // Add critical coal gen responses to VM
                foreach (GeneratingStationForOtherReason gen in otherReasonGens)
                {
                    bool respExists = false;
                    bool yestRespExists = false;
                    foreach (OtherReasonsResponse resp in existingResponses.Where(r => r.Station == gen.Name))
                    {
                        resp.SerialNum = gen.SerialNum;
                        vm.OtherReasonsResponses.Add(resp);
                        respExists = true;
                    }
                    if (usrIsAdmin && !respExists)
                    {
                        // populate yesterday data only if user is in admin role
                        foreach (OtherReasonsResponse resp in yestResponses.Where(r => r.Station == gen.Name))
                        {
                            resp.SerialNum = gen.SerialNum;
                            resp.Id = 0;
                            vm.OtherReasonsResponses.Add(resp);
                            yestRespExists = true;
                        }
                    }
                    if (!respExists && !yestRespExists)
                    {
                        // If there is no existing response for gen, then add a new response
                        vm.OtherReasonsResponses.Add(new OtherReasonsResponse()
                        {
                            DataDate = entryDate,
                            SerialNum = gen.SerialNum,
                            Station = gen.Name,
                            Location = gen.Location,
                            Agency = gen.Agency,
                            Capacity = gen.Capacity,
                            Remarks = ""
                        });
                    }

                }
            }

            // fetch existing CriticalCoalResponses and initialize placeholder responses if any gens are assigned to the user
            if (criticalCoalGens.Count > 0)
            {
                List<CriticalCoalResponse> existingResponses = await _context.CriticalCoalResponses.Where(r => r.DataDate == entryDate).ToListAsync();
                List<CriticalCoalResponse> yestResponses = await _context.CriticalCoalResponses.Where(r => r.DataDate == entryDate.AddDays(-1)).ToListAsync();
                // Add critical coal gen responses to VM
                foreach (GeneratingStationForCriticalCoal gen in criticalCoalGens)
                {
                    bool respExists = false;
                    bool yestRespExists = false;
                    foreach (CriticalCoalResponse resp in existingResponses.Where(r => r.Station == gen.Name))
                    {
                        resp.SerialNum = gen.SerialNum;
                        vm.CriticalCoalResponses.Add(resp);
                        respExists = true;
                    }
                    if (usrIsAdmin && !respExists)
                    {
                        // populate yesterday data only if user is in admin role
                        foreach (CriticalCoalResponse resp in yestResponses.Where(r => r.Station == gen.Name))
                        {
                            resp.SerialNum = gen.SerialNum;
                            resp.Id = 0;
                            vm.CriticalCoalResponses.Add(resp);
                            yestRespExists = true;
                        }
                    }
                    if (!respExists && !yestRespExists)
                    {
                        // If there is no existing response for gen, then add a new response
                        vm.CriticalCoalResponses.Add(new CriticalCoalResponse()
                        {
                            DataDate = entryDate,
                            SerialNum = gen.SerialNum,
                            Station = gen.Name,
                            Owner = gen.Owner,
                            Capacity = gen.Capacity,
                            Remarks = ""
                        });
                    }

                }
            }

            return vm;
        }
    }
}