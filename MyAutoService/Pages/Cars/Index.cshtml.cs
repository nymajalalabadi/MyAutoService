using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model.ViewModels;

namespace MyAutoService.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustomerViewModel { get; set; }


        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var ClaimIdentity = (ClaimsIdentity)User.Identity;
                var Claim = ClaimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = Claim.Value;
            }

            CarAndCustomerViewModel = new CarAndCustomerViewModel()
            {
                Cars =await _db.Cars.Where(u => u.UserId == userId).ToListAsync(),
                User=await _db.ApplicationUsers.FirstOrDefaultAsync(u=>u.Id==userId)
            };

            return Page();
        }
    }
}
