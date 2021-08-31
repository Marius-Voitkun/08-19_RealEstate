using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _08_19_RealEstate.Models
{
    public class Broker
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        public List<Company> Companies { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
