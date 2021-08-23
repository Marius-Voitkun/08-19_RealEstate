using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _08_19_RealEstate.Services
{
    public class CompaniesBrokersDbService
    {
        private readonly SqlConnection _connection;

        public CompaniesBrokersDbService(SqlConnection connection)
        {
            _connection = connection;
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

            using (_connection)
            {
                _connection.Execute(query);
            }
        }
    }
}
