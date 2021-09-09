using _08_19_RealEstate.Data;
using _08_19_RealEstate.Models;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class BrokersDbService
    {
        private DataContext _context;

        public BrokersDbService(DataContext context)
        {
            _context = context;
        }

        public List<Broker> GetBrokers(List<int> brokersIds = null)
        {
            if (brokersIds != null && brokersIds.Count != 0)
            {
                return _context.Brokers.Where(b => brokersIds.Contains(b.Id)).ToList();
            }

            return _context.Brokers.ToList();
        }

        public void AddBroker(Broker broker)
        {
            _context.Brokers.Add(broker);
            _context.SaveChanges();
        }

        public void UpdateBroker(Broker broker)
        {
            _context.Brokers.Update(broker);
            _context.SaveChanges();
        }

        public void DeleteBroker(int id)
        {
            Broker broker = _context.Brokers.Single(b => b.Id == id);

            _context.Brokers.Remove(broker);
            _context.SaveChanges();
        }
    }
}
