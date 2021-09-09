using System.ComponentModel.DataAnnotations;

namespace _08_19_RealEstate.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; }

        [Required]
        [StringLength(7)]
        public string HouseNr { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [StringLength(5)]
        public string FlatNr { get; set; }

        public override string ToString()
        {
            string flat = (FlatNr != null && FlatNr != "") ? ('/' + FlatNr) : "";

            return $"{Street} {HouseNr}{flat}, {City}";
        }

        public string ToStringWithoutCity()
        {
            string flat = (FlatNr != null && FlatNr != "") ? ('/' + FlatNr) : "";

            return $"{Street} {HouseNr}{flat}";
        }
    }
}
