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
        private readonly SqlConnection _connection;
        private readonly IConfiguration _configuration;

        public BrokersDbService(SqlConnection connection, IConfiguration configuration)
        {
            _connection = connection;
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

            //using (_connection)
            //{
            //    brokers = _connection.Query<Broker>(query).ToList();
            //}
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _configuration.GetConnectionString("Default");

                brokers = _connection.Query<Broker>(query).ToList();
            }

            return brokers;
        }

        public void AddBroker(Broker broker)
        {
            string query = @$"INSERT INTO dbo.Brokers (FirstName, LastName)
                              VALUES (N'{broker.FirstName}', N'{broker.LastName}');";

            using (_connection)
            {
                _connection.Execute(query);
            }
        }
    }
}
