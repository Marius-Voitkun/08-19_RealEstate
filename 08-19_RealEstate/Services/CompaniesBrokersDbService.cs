using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesBrokersDbService
    {
        private readonly IConfiguration _configuration;

        public CompaniesBrokersDbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CompanyBrokerJunction> GetJunctions()
        {
            List<CompanyBrokerJunction> companiesBrokers = new();

            string query = "SELECT * FROM dbo.CompaniesBrokers";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                companiesBrokers = connection.Query<CompanyBrokerJunction>(query).ToList();
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
            string query = $"SELECT BrokerId FROM dbo.CompaniesBrokers WHERE CompanyId = {companyId};";

            List<int> brokersIds = new();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                brokersIds = connection.Query<int>(query).ToList();
            }

            return brokersIds;
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
