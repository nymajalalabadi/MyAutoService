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
    public class HistoryModel : PageModel
    {

        private ApplicationDbContext _db;

        public HistoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<ServiceHeader> serviceHeaders { get; set; }

        public string   UserId { get; set; }
        public void OnGet(int carId)
        {
            serviceHeaders = _db.ServiceHeaders
                .Include(c => c.Car)
                .Include(c => c.Car.ApplicationUser)
                .Where(c => c.CarId == carId).ToList();

            UserId = _db.Cars.Where(c => c.Id == carId).FirstOrDefault().UserId;

        }
    }
}
