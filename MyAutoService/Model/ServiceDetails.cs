using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoService.Model
{
    public class ServiceDetails
    {
        public int Id { get; set; }


        public int ServiceHeaderId { get; set; }

        [ForeignKey("ServiceHeader")]
        public virtual ServiceHeader Header { get; set; }



        [Display(Name = "سرویس")]
        public int ServiceTypeId { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }



        public double ServicePrice { get; set; }

        public string ServiceName { get; set; }
    }
}
