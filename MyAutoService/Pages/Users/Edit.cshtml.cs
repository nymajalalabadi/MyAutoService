using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model;
using MyAutoService.Model.ViewModels;
using MyAutoService.Utlities;

namespace MyAutoService.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        [BindProperty]
        public string SelectedRole { get; set; }

        public SelectList Roles { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if (id.Trim().Length == 0)
            {
                return NotFound();
            }

            ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);

            if (ApplicationUser == null)
            {
                NotFound();
            }
            var userRoles = _userManager.GetRolesAsync(new IdentityUser()
            { 
                Id = ApplicationUser.Id 

            }).Result;//(ClaimsIdentity) User.Identity;

            Roles = new SelectList(_roleManager.Roles, "Name", "Name", userRoles.First());

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                NotFound();
            }

            var userInDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == ApplicationUser.Id);
            if(userInDb==null)
            {
                NotFound();
            }

            userInDb.Name = ApplicationUser.Name;
            userInDb.Address = ApplicationUser.Address;
            userInDb.PhoneNumber = ApplicationUser.PhoneNumber;
            userInDb.PostalCode = ApplicationUser.PostalCode;
            userInDb.City = ApplicationUser.City;

            var userRoles = _userManager.GetRolesAsync(new IdentityUser()
            { 
                Id=ApplicationUser.Id
            }).Result;

            if (SelectedRole != userRoles.FirstOrDefault())
            {
                await _userManager.RemoveFromRoleAsync(new IdentityUser()
                {
                    Id = ApplicationUser.Id
                },userRoles.FirstOrDefault());
                await _userManager.AddToRoleAsync(new IdentityUser()
                {
                    Id = ApplicationUser.Id
                }, SelectedRole);
            }

            _db.Update(userInDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
