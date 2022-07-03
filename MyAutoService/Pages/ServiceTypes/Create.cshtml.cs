using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Model;
using MyAutoService.Data;
using Microsoft.AspNetCore.Authorization;
using MyAutoService.Utlities;

namespace MyAutoService.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class CreateModel : PageModel
    {

        private ApplicationDbContext _dbcontext;

        public CreateModel(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ServiceType ServiceType { get; set; }

        public IActionResult OnGet()
        {
            
            return Page();
        }

        public async Task<IActionResult> OnPost(ServiceType serviceType)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _dbcontext.ServiceType.Add(serviceType);
             await _dbcontext.SaveChangesAsync();
            return RedirectToPage("Index");

        }

    }
}
