using System.ComponentModel.DataAnnotations.Schema;

namespace _08_19_RealEstate.Models
{
    [Table("CompaniesBrokers")]
    public class CompanyBrokerJunction
    {
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int BrokerId { get; set; }

        public Broker Broker { get; set; }
    }
}
