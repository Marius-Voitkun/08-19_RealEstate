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
        private readonly AddressesDbService _addressesDbService;
        private readonly IConfiguration _configuration;

        public CompaniesDbService(AddressesDbService addressesDbService, IConfiguration configuration)
        {
            _addressesDbService = addressesDbService;
            _configuration = configuration;
        }

        public List<Company> GetCompanies()
        {
            List<Company> companies = new();

            string query = "SELECT * FROM dbo.Companies;";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                companies = connection.Query<Company>(query).ToList();
            }

            List<Address> addresses = _addressesDbService.GetAddresses();

            foreach (var company in companies)
            {
                company.Address = addresses.Single(a => a.Id == company.AddressId);
            }

            return companies;
        }

        public int AddCompanyAndGetId(Company company)
        {
            int addressId = _addressesDbService.AddAddressAndGetItsId(company.Address);

            string query = $@"INSERT INTO dbo.Companies (Name, AddressId)
                              VALUES (N'{company.Name}', {addressId});
                              SELECT CAST(SCOPE_IDENTITY() AS INT);";

            int companyId = 0;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                companyId = connection.Query<int>(query).ToList()[0];
            }

            return companyId;
        }

        public void UpdateCompany(Company company)
        {
            _addressesDbService.UpdateAddress(company.Address);

            string query = $@"UPDATE dbo.Companies
                              SET Name = N'{company.Name}'
                              WHERE Id = {company.Id};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }

        public void DeleteCompany(int id)
        {
            string query = $"DELETE FROM dbo.Companies WHERE Id = {id};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }
    }
}
