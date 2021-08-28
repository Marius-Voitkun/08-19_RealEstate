using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.Models
{
    public class ApartmentsFilterModel
    {
        public int? CompanyId { get; set; }

        public int? BrokerId { get; set; }

        public string City { get; set; }
    }
}
