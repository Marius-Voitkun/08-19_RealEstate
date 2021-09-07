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
        private readonly IConfiguration _configuration;

        public AddressesDbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Address> GetAddresses()
        {
            List<Address> addresses = new();

            string query = "SELECT * FROM dbo.Addresses;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                addresses = connection.Query<Address>(query).ToList();
            }

            return addresses;
        }

        public List<string> GetCities()
        {
            return GetAddresses().Select(a => a.City).Distinct().ToList();
        }

        public int AddAddressAndGetItsId(Address address)
        {
            string query = $@"INSERT INTO dbo.Addresses (City, Street, HouseNr, FlatNr)
                              VALUES (N'{address.City}', N'{address.Street}', '{address.HouseNr}', '{address.FlatNr}');
                              SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int addressId = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                addressId = connection.Query<int>(query).ToList()[0];
            }

            return addressId;
        }

        public void UpdateAddress(Address address)
        {
            string query = $@"UPDATE dbo.Addresses
                              SET City = N'{address.City}', Street = N'{address.Street}', HouseNr = '{address.HouseNr}', FlatNr = '{address.FlatNr}'
                              WHERE Id = {address.Id};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }

        public void DeleteAddress(int id)
        {
            string query = $"DELETE FROM dbo.Addresses WHERE Id = {id};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }
    }
}
