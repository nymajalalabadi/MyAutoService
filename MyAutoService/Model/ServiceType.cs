using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyAutoService.Model
{
    public class ServiceType
    {
        public int Id { get; set; }

        [Display(Name = "نام سرویس")]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Display(Name = "قیمت سرویس")]
        [Required(ErrorMessage = "لطفا قیمت را وارد کنید")]
        public double Price { get; set; }

    }
}
