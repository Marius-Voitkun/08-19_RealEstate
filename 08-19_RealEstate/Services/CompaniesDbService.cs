using _08_19_RealEstate.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesDbService
    {
        private readonly SqlConnection _connection;
        private readonly AddressesDbService _addressesDbService;
        private readonly IConfiguration _configuration;

        public CompaniesDbService(SqlConnection connection, AddressesDbService addressesDbService, IConfiguration configuration)
        {
            _connection = connection;
            _addressesDbService = addressesDbService;
            _configuration = configuration;
        }

        public List<Company> GetCompanies()
        {
            List<Company> companies = new();

            string query = "SELECT * FROM dbo.Companies;";

            //using (_connection)
            //{
            //    companies = _connection.Query<Company>(query).ToList();
            //}

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("Default");

                companies = _connection.Query<Company>(query).ToList();
            }

            List<Address> addresses = _addressesDbService.GetAddresses();

            foreach (var company in companies)
            {
                company.Address = addresses.Single(a => a.Id == company.AddressId);
            }

            return companies;
        }

        public int AddCompanyAndGetItsId(Company company)
        {
            int addressId = _addressesDbService.AddAddressAndGetItsId(company.Address);

            string query = $@"INSERT INTO dbo.Companies (Name, AddressId)
                              VALUES (N'{company.Name}', {addressId});
                              SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int companyId = 0;

            using (_connection)
            {
                companyId = _connection.Query<int>(query).ToList()[0];
            }

            return companyId;
        }

        public void UpdateCompany(Company company)
        {
            _addressesDbService.UpdateAddress(company.Address);

            string query = $@"UPDATE dbo.Companies
                              SET Name = N'{company.Name}'
                              WHERE Id = {company.Id};";

            using (_connection)
            {
                _connection.Execute(query);
            }
        }
    }
}
