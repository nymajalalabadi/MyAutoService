using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoService.Model.ViewModels
{
    public class LoggedInUserViewModel
    {

        public string Name { get; set; }
        public List<ServicesShoppingCart> ShoppingCart { get; set; }

    }
}
