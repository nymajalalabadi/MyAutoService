using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model;

namespace MyAutoService.Pages.Services
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceHeader ServiceHeader { get; set; }

        public List<ServiceDetails> serviceDetails { get; set; }

        public IActionResult OnGet(int serviceid)
        {
            ServiceHeader = _db.ServiceHeaders
                .Include(s => s.Car)
                .Include(s => s.Car.ApplicationUser)
                .FirstOrDefault(s => s.Id == serviceid);

            if (ServiceHeader == null)
                return NotFound();

            serviceDetails = _db.ServiceDetails.Where(d => d.ServiceHeaderId == serviceid).ToList();

            return Page();
        }
    }
}
