using _08_19_RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.ViewModels
{
    public class CompanyFormViewModel
    {
        public Company Company { get; set; }

        public List<Broker> Brokers { get; set; }

        public List<int> SelectedBrokersIds { get; set; } = new();
    }
}
