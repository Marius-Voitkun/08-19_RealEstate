using System.ComponentModel.DataAnnotations;

namespace _08_19_RealEstate.Models
{
    public class Apartment
    {
        public int Id { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value is invalid.")]    // error message not working
        public int Floor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value is invalid.")]    // error message not working
        public int TotalFloorsInBuilding { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The value is invalid.")]    // error message not working
        public decimal AreaInSqm { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a broker.")]    // error message not working
        public int BrokerId { get; set; }

        public Broker Broker { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a company.")]   // error message not working
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
