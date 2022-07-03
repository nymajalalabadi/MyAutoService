using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model;
using MyAutoService.Model.ViewModels;

namespace MyAutoService.Pages.Services
{
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public CarsServiceViewModel CarServiceVM { get; set; }


        public async Task<IActionResult> OnGet(int carId)
        {
            CarServiceVM = new CarsServiceViewModel()
            {
                Car = await _db.Cars.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.Id == carId),
                ServiceHeader = new ServiceHeader()
            };

            List<string> lstServiceTypeInShoppingCart = _db.ServicesShoppingCarts
                .Include(c => c.ServiceType)
                .Where(c => c.CarId == carId)
                .Select(c => c.ServiceType.Name).ToList();
                

            IQueryable<ServiceType> lstServices = from s in _db.ServiceType
                                                  where !(lstServiceTypeInShoppingCart.Contains(s.Name))
                                                  select s;

            CarServiceVM.ServiceTypesList = lstServices.ToList();

            CarServiceVM.ServicesShoppingCarts = _db.ServicesShoppingCarts.Include(m => m.ServiceType)
                .Where(c => c.CarId == carId).ToList();

            CarServiceVM.ServiceHeader.TotalPrice = 0;

            foreach (var item in CarServiceVM.ServicesShoppingCarts)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }
            
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                CarServiceVM.ServiceHeader.DateAdded = DateTime.Now;

                CarServiceVM.ServicesShoppingCarts = _db.ServicesShoppingCarts.Include(c => c.ServiceType)
                    .Where(c => c.CarId == CarServiceVM.Car.Id).ToList();

                foreach (var shop in CarServiceVM.ServicesShoppingCarts)
                {
                    CarServiceVM.ServiceHeader.TotalPrice += shop.ServiceType.Price;
                }

                CarServiceVM.ServiceHeader.CarId = CarServiceVM.Car.Id;
                _db.ServiceHeaders.Add(CarServiceVM.ServiceHeader);
                await _db.SaveChangesAsync();

                foreach (var shop in CarServiceVM.ServicesShoppingCarts)
                {
                    ServiceDetails details = new ServiceDetails()
                    {
                        ServiceHeaderId = CarServiceVM.ServiceHeader.Id,
                        ServiceName = shop.ServiceType.Name,
                        ServicePrice = shop.ServiceType.Price,
                        ServiceTypeId = shop.ServiceTypeId
                    };
                    _db.ServiceDetails.Add(details);
                }
                _db.ServicesShoppingCarts.RemoveRange(CarServiceVM.ServicesShoppingCarts);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Cars/Index", new { userId = CarServiceVM.Car.UserId });
            }


            return Page();
        }


        public async Task<IActionResult> OnPostAddToCart()
        {
            ServicesShoppingCart Shopping = new ServicesShoppingCart()
            {
                CarId = CarServiceVM.Car.Id,
                ServiceTypeId=CarServiceVM.ServiceDetails.ServiceTypeId
            };
            _db.ServicesShoppingCarts.Add(Shopping);
            await _db.SaveChangesAsync();

            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }


        public async Task<IActionResult> OnPostRemoveFromCart(int serviceTypeId)
        {
            ServicesShoppingCart shopping = _db.ServicesShoppingCarts
                .First(u => u.CarId == CarServiceVM.Car.Id && u.ServiceTypeId == serviceTypeId);

            _db.Remove(shopping);

            await _db.SaveChangesAsync();

            return RedirectToPage("Create", new { carId = CarServiceVM.Car.Id });
        }
    }
}
