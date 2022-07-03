using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model;
using MyAutoService.Utlities;

namespace MyAutoService.Pages.ServiceTypes
{
    [Authorize (Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<ServiceType> ServiceTypes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ServiceTypes = await _dbContext.ServiceType.ToListAsync();
            return Page();
        }
    }
}
