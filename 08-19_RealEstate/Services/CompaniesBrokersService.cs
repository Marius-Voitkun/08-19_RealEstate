using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesBrokersService
    {
        private DataContext _context;
        private readonly IConfiguration _configuration;

        public CompaniesBrokersService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<CompanyBrokerJunction> GetJunctions()
        {
            List<Company> companies = _context.Companies.Include(c => c.Brokers).ToList();

            List<CompanyBrokerJunction> companiesBrokers = new();

            foreach (var company in companies)
            {
                foreach (var broker in company.Brokers)
                {
                    companiesBrokers.Add(new CompanyBrokerJunction
                    {
                        CompanyId = company.Id,
                        BrokerId = broker.Id
                    });
                }
            }

            return companiesBrokers;
        }

        public void AddJunctions(int companyId, List<int> brokerIds)
        {
            if (brokerIds.Count == 0)
                return;

            List<string> valueSets = new();

            foreach (int brokerId in brokerIds)
                valueSets.Add($"({companyId}, {brokerId})");

            string valueSetsJoined = string.Join(", ", valueSets);

            string query = @$"INSERT INTO dbo.CompaniesBrokers (CompanyId, BrokerId)
                              VALUES {valueSetsJoined};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }

        public List<int> GetBrokersIdsForCompany(int companyId)
        {
            return _context.Companies.Include(c => c.Brokers).SingleOrDefault(c => c.Id == companyId).Brokers.Select(b => b.Id).ToList();
        }

        public bool DeleteJunctionsByCompany(int companyId)
        {
            string query = $"DELETE FROM dbo.CompaniesBrokers WHERE CompanyId = {companyId};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteJunctionsByBroker(int brokerId)
        {
            string query = $"DELETE FROM dbo.CompaniesBrokers WHERE BrokerId = {brokerId};";

            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    connection.Execute(query);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
