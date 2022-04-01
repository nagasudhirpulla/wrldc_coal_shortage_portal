using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoalShortagePortal.Core.Entities;
using CoalShortagePortal.Core.ReportingData;
using CoalShortagePortal.Data;
using CoalShortagePortal.Infrastructure.Services.ReportingData;
using CoalShortagePortal.WebApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoalShortagePortal.WebApp.Pages.UnrevGens
{
    [Authorize]
    public class UnRevGensModel : PageModel
    {
        private readonly ILogger<UnRevGensModel> _logger;
        private readonly ReportingDataService _reportingService;
        private readonly ApplicationDbContext _context;

        public UnRevGensModel(ILogger<UnRevGensModel> logger, ReportingDataService reportingDataService, ApplicationDbContext context)
        {
            _logger = logger;
            _reportingService = reportingDataService;
            _context = context;
        }

        [BindProperty]
        public DateTime RecordDate { get; set; } = DateTime.Now.Date;

        [BindProperty]
        public List<ExpectedRevivalResponse> RevivalResponses { get; set; } = new();

        public async Task OnGetAsync()
        {
            DateTime entryDate = DateTime.Now.Date;
            await PopulateResponses(entryDate);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RevivalResponses = new();
            await PopulateResponses(RecordDate);
            return Page();
        }

        private static ExpectedRevivalResponse Sanitize(ExpectedRevivalResponse resp)
        {
            resp.ElementOwners = TextUtils.SanitizeText(resp.ElementOwners);
            resp.ElementName = TextUtils.SanitizeText(resp.ElementName);
            resp.OutageReason = TextUtils.SanitizeText(resp.OutageReason);
            resp.OutageType = TextUtils.SanitizeText(resp.OutageType);
            resp.Remarks = TextUtils.SanitizeText(resp.Remarks);
            return resp;
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // update existing records and create new records
            foreach (ExpectedRevivalResponse resp in RevivalResponses)
            {
                // input sanitization
                ExpectedRevivalResponse sanitResp = Sanitize(resp);
                // check if resp is to be added or inserted
                if (sanitResp.Id != 0)
                {
                    _context.Update(sanitResp);
                }
                else
                {
                    sanitResp.DataDate = RecordDate;
                    _context.ExpectedRevivalResponses.Add(sanitResp);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(nameof(Index));
        }

        private async Task PopulateResponses(DateTime entryDate)
        {
            // https://github.com/nagasudhirpulla/wrldc_scada_issues_portal/blob/master/src/WrldcScadaIssuesPortal/ScadaIssuesPortal.Web/Views/ReportingCases/Create.cshtml
            // get latest unrevived generator outages information
            List<UnRevGeneratorOutage> genOutages = _reportingService.GetLatestUnrevivedGenOutages(entryDate);

            // get existing responses for the date
            List<ExpectedRevivalResponse> existingResponses = await _context.ExpectedRevivalResponses.Where(r => r.DataDate == entryDate).ToListAsync();
            // get yesterday responses
            List<ExpectedRevivalResponse> yestResponses = await _context.ExpectedRevivalResponses.Where(r => r.DataDate == entryDate.AddDays(-1)).ToListAsync();

            foreach (UnRevGeneratorOutage outage in genOutages)
            {
                bool todayRespExists = false;

                // check if existing response is present for this outage
                foreach (ExpectedRevivalResponse resp in existingResponses.Where(r => r.RTOutageId == outage.RTOutageId))
                {
                    RevivalResponses.Add(resp);
                    todayRespExists = true;
                }

                // check if yesterday response exists for this outage if today response was not present
                bool yestRespExists = false;
                if (!todayRespExists)
                {
                    foreach (ExpectedRevivalResponse resp in yestResponses.Where(r => r.RTOutageId == outage.RTOutageId))
                    {
                        RevivalResponses.Add(resp);
                        yestRespExists = true;
                    }
                }

                // create placeholder response if response was not found for this outage either today or yesterday
                if (!todayRespExists && !yestRespExists)
                {
                    RevivalResponses.Add(new ExpectedRevivalResponse()
                    {
                        DataDate = entryDate,
                        RTOutageId = outage.RTOutageId,
                        ElementOwners = outage.ElementOwners,
                        ElementName = outage.ElementName,
                        InstalledCapacity = outage.InstalledCapacity,
                        OutageReason = outage.OutageReason,
                        OutageType = outage.OutageType,
                        OutageDateTime = outage.OutageDateTime,
                        ExpectedRevivalTime = outage.ExpectedDateTime
                    });
                }
            }

        }
    }
}
