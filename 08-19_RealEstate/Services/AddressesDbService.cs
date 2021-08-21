using _08_19_RealEstate.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace _08_19_RealEstate.Services
{
    public class AddressesDbService
    {
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public AddressesDbService(SqlConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public List<Address> GetAddresses()
        {
            List<Address> addresses = new();

            string query = "SELECT * FROM dbo.Addresses;";

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("Default");

                addresses = connection.Query<Address>(query).ToList();
            }

            return addresses;
        }

        public int AddAddressAndGetItsId(Address address)
        {
            string query = $@"INSERT INTO dbo.Addresses (City, Street, HouseNr, FlatNr)
                              VALUES (N'{address.City}', N'{address.Street}', '{address.HouseNr}', '{address.FlatNr}');
                              SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int addressId = 0;

            using (_connection)
            {
                addressId = _connection.Query<int>(query).ToList()[0];
            }

            return addressId;
        }
    }
}
