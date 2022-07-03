using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Model;

namespace MyAutoService.Pages.Cars
{
    public class EditModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public EditModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }
        [BindProperty]
        public IFormFile ImgUp { get; set; }
        public async Task<IActionResult> OnGetAsync(int? carId)
        {
            if (carId == null)
            {
                return NotFound();
            }

            Car = await _context.Cars
                .Include(c => c.ApplicationUser).FirstOrDefaultAsync(m => m.Id == carId);

            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImgUp != null)
            {
                Car.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    ImgUp.CopyTo(fileStream);
                }
            }

            if (ImgUp != null)
            {
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }


                Car.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    ImgUp.CopyTo(fileStream);
                }
            }
            _context.Attach(Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
