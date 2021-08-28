using _08_19_RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.ViewModels
{
    public class ApartmentsOfBrokerViewModel
    {
        public List<Apartment> Apartments { get; set; }

        public List<string> Cities { get; set; }

        public ApartmentsFilterModel FilterModel { get; set; }
    }
}
