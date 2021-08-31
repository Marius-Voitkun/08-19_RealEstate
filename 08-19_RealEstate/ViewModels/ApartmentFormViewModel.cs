using _08_19_RealEstate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace _08_19_RealEstate.ViewModels
{
    public class ApartmentFormViewModel
    {
        public Apartment Apartment { get; set; }
        
        public List<Company> Companies { get; set; }

        public List<Broker> Brokers { get; set; }

        public string BrokersJson { get; set; }

        public string CompaniesBrokersJson { get; set; }
    }
}
