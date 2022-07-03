using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyAutoService.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "نام مشتری")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "ادرس")]
        public string Address { get; set; }


        [Display(Name = "شهر")]
        [MaxLength(200)]
        public string City { get; set; }

        [Display(Name = "کد پستی")]
        [MaxLength(200)]
        public string PostalCode { get; set; }

        [Display(Name = "ایمیل")]
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        [Display(Name = "تلفن")]
        public override string PhoneNumber
        {
            get { return base.PhoneNumber; }
            set { base.PhoneNumber = value; }
        }


        public virtual List<Car> cars { get; set; }

    }
}
