using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoService.Model
{
    public class ServiceHeader
    {
        public int Id { get; set; }

        [Display(Name = " کیلومتر")]
        public double Miles { get; set; }

        [Required]
        [Display(Name ="جمع کل")]
        public double TotalPrice { get; set; }

        [Display(Name = " تویحات")]
        public string Description { get; set; }

        [Required]
        [Display(Name = " ناریخ")]
        [DisplayFormat(DataFormatString = "yyyy/MM/dd")]
        public DateTime DateAdded { get; set; }


        public int CarId { get; set; }


        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
    }
}
