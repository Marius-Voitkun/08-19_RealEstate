using System.ComponentModel.DataAnnotations;

namespace _08_19_RealEstate.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public int Floor { get; set; }

        public int TotalFloorsInBuilding { get; set; }

        public int AreaInSqm { get; set; }

        [Required]
        public int BrokerId { get; set; }

        public Broker Broker { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
