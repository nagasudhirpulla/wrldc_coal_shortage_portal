using System.Collections.Generic;
using CoalShortagePortal.Core.ReportingData;
using CoalShortagePortal.Infrastructure.Services.ReportingData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoalShortagePortal.WebApp.Pages.UnrevGens
{
    [Authorize]
    public class UnRevGensModel : PageModel
    {
        private readonly ILogger<UnRevGensModel> _logger;
        private readonly ReportingDataService _reportingService;

        public UnRevGensModel(ILogger<UnRevGensModel> logger, ReportingDataService reportingDataService)
        {
            _logger = logger;
            _reportingService = reportingDataService;
        }

        public List<UnRevGeneratorOutage> GenOutages { get; set; } = new();

        public void OnGet()
        {
            GenOutages = _reportingService.GetLatestUnrevivedGenOutages();
            // https://github.com/nagasudhirpulla/wrldc_scada_issues_portal/blob/master/src/WrldcScadaIssuesPortal/ScadaIssuesPortal.Web/Views/ReportingCases/Create.cshtml
        }
    }
}
