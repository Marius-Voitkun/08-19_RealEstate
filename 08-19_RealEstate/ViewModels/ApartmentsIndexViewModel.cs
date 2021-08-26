using _08_19_RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.ViewModels
{
    public class ApartmentsIndexViewModel
    {
        public List<Apartment> Apartments { get; set; }

        public List<Company> Companies { get; set; }

        public List<Broker> Brokers { get; set; }

        public List<string> Cities { get; set; }

        public int? CompanyIdForFiltering { get; set; }

        public int? BrokerIdForFiltering { get; set; }

        public string CityForFiltering { get; set; }
    }
}
