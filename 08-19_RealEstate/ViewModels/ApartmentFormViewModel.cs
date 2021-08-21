using _08_19_RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.ViewModels
{
    public class ApartmentFormViewModel
    {
        public Apartment Apartment { get; set; }

        public List<Broker> Brokers { get; set; }

        public List<Company> Companies { get; set; }
    }
}
