using _08_19_RealEstate.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesBrokersDbService
    {
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public CompaniesBrokersDbService(SqlConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public void AddCompaniesBrokersJunctions(int companyId, List<int> brokerIds)
        {
            if (brokerIds.Count == 0)
                return;

            List<string> valueSets = new();

            foreach (int brokerId in brokerIds)
                valueSets.Add($"({companyId}, {brokerId})");

            string valueSetsJoined = string.Join(", ", valueSets);

            string query = @$"INSERT INTO dbo.CompaniesBrokers (CompanyId, BrokerId)
                              VALUES {valueSetsJoined};";

            //using (_connection)
            //{
            //    _connection.Execute(query);
            //}

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("Default");

                connection.Execute(query);
            }
        }

        public List<int> GetBrokersIdsForCompany(int companyId)
        {
            string query = $"SELECT BrokerId FROM dbo.CompaniesBrokers WHERE CompanyId = {companyId};";

            List<int> brokersIds = new();

            using (_connection)
            {
                brokersIds = _connection.Query<int>(query).ToList();
            }

            return brokersIds;
        }

        public void DeleteCompaniesBrokersJunctions(int companyId)
        {
            string query = $"DELETE FROM dbo.CompaniesBrokers WHERE CompanyId = {companyId};";

            using (_connection)
            {
                _connection.Execute(query);
            }
        }
    }
}
