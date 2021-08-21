using _08_19_RealEstate.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class BrokersDbService
    {
        private readonly SqlConnection _connection;

        public BrokersDbService(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<Broker> GetBrokers()
        {
            List<Broker> brokers = new();

            string query = "SELECT * FROM dbo.Brokers;";

            using (_connection)
            {
                brokers = _connection.Query<Broker>(query).ToList();
            }

            return brokers;
        }
    }
}
