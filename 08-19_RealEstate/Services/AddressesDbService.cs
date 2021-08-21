using _08_19_RealEstate.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class AddressesDbService
    {
        private readonly SqlConnection _connection;

        public AddressesDbService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Address> GetAddresses()
        {
            List<Address> addresses = new();

            string query = "SELECT * FROM dbo.Addresses;";

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=localhost;Database=RealEstate;Trusted_Connection=True;";

                addresses = connection.Query<Address>(query).ToList();
            }

            return addresses;
        }
    }
}
