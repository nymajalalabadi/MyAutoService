using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model.ViewModels;

namespace MyAutoService.ViewComponents
{
    public class LoggedInUserViewComponent : ViewComponent
    {
        private ApplicationDbContext _context;

        public LoggedInUserViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            LoggedInUserViewModel logged = new LoggedInUserViewModel()
            {
                Name = _context.ApplicationUsers.First(u => u.Email == User.Identity.Name).Name,

                ShoppingCart = _context.ServicesShoppingCarts
                    .Include(c => c.Car).ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.ServiceType)
                    .Where(u => u.Car.ApplicationUser.Email == User.Identity.Name)
                    .ToList()
            };
            return View("~/Pages/Shared/Components/LoggedInUser.cshtml", logged);
        }
    }
}
