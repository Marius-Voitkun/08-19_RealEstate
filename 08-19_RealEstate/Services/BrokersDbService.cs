using _08_19_RealEstate.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class BrokersDbService
    {
        private readonly IConfiguration _configuration;

        public BrokersDbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Broker> GetBrokers(List<int> brokersIds = null)
        {
            List<Broker> brokers = new();
            string queryFragmentToFilterByIds = "";
            
            if (brokersIds != null && brokersIds.Count != 0)
            {
                string brokersIdsJoined = string.Join(", ", brokersIds);
                queryFragmentToFilterByIds = $"WHERE Id IN ({brokersIdsJoined})";
            }

            string query = @$"SELECT * FROM dbo.Brokers
                              {queryFragmentToFilterByIds};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                brokers = connection.Query<Broker>(query).ToList();
            }

            return brokers;
        }

        public void AddBroker(Broker broker)
        {
            string query = @$"INSERT INTO dbo.Brokers (FirstName, LastName)
                              VALUES (N'{broker.FirstName}', N'{broker.LastName}');";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }

        public void UpdateBroker(Broker broker)
        {
            string query = @$"UPDATE dbo.Brokers
                              SET FirstName = N'{broker.FirstName}', LastName = N'{broker.LastName}'
                              WHERE Id = {broker.Id};";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Execute(query);
            }
        }
    }
}
