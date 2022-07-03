using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoService.Model.ViewModels
{
    public class CarsServiceViewModel
    {
        public Car Car { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
        public ServiceDetails ServiceDetails { get; set; }

        public List<ServiceType> ServiceTypesList { get; set; }
        public List<ServicesShoppingCart> ServicesShoppingCarts { get; set; }

    }
}
