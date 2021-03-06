using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public List<Broker> Brokers { get; set; }
    }
}
