using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _08_19_RealEstate.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Broker
    {
        [JsonProperty]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the first name.")]
        [StringLength(50)]
        [JsonProperty]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name.")]
        [StringLength(50)]
        [JsonProperty]
        public string LastName { get; set; }

        public List<Company> Companies { get; set; }

        public List<Apartment> Apartments { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
