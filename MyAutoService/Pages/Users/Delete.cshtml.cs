using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Data;
using MyAutoService.Model;
using MyAutoService.Utlities;

namespace MyAutoService.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public DeleteModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if(id.Trim().Length==0)
            {
                NotFound();
            }

            ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);

            if(ApplicationUser==null)
            {
                NotFound();
            }
                      
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser = await _db.ApplicationUsers.FindAsync( id);

            if (ApplicationUser != null)
            {
                _db.ApplicationUsers.Remove(ApplicationUser);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
