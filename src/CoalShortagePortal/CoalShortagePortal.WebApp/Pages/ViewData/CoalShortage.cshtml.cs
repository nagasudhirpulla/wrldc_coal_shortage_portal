using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoalShortagePortal.Core.Entities;
using CoalShortagePortal.Data;
using CoalShortagePortal.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoalShortagePortal.WebApp.Pages.ViewData
{
    public class CoalShortageModel : PageModel
    {
        [Required]
        [BindProperty]
        public DateTime StartDate { get; set; }
        [Required]
        [BindProperty]
        public DateTime EndDate { get; set; }
        [Required]
        [BindProperty]
        public string Generator { get; set; }

        public GenDataDTO GenData { get; set; }

        private readonly ApplicationDbContext _context;

        public CoalShortageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            ViewData["Generator"] = new SelectList(_context.GeneratingStationForCoalShortages, "Name", "Name");
            EndDate = DateTime.Now.AddDays(-1);
            StartDate = DateTime.Now.AddMonths(-1);
            GenData = new GenDataDTO();
        }

        public async Task OnPostAsync()
        {
            ViewData["Generator"] = new SelectList(_context.GeneratingStationForCoalShortages, "Name", "Name");
            GenData = new GenDataDTO();
            List<CoalShortageResponse> coalShortageData = await _context.CoalShortageResponses
                                    .Where(cr => (cr.DataDate >= StartDate.Date)
                                        && (cr.DataDate <= EndDate.Date)
                                        && cr.Station.Equals(Generator))
                                    .OrderBy(cr => cr.DataDate)
                                    .ToListAsync();
            GenData.Data.Add("Coal_Shortage", coalShortageData.Select(cd => cd.GenLossMw).ToList());
            GenData.Data.Add("Average_Generation", coalShortageData.Select(cd => cd.PrevDayAvgMw).ToList());
            GenData.Data.Add("Capacity", coalShortageData.Select(cd => cd.Capacity).ToList());
            GenData.Timestamps = coalShortageData.Select(cd => cd.DataDate.Date).ToList();
        }
    }
}