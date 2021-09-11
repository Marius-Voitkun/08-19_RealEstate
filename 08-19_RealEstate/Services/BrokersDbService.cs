using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class BrokersDbService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrokersDbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Broker> GetBrokers(List<int> brokersIds = null)
        {
            //if (brokersIds != null && brokersIds.Count != 0)
            //{
            //    return _context.Brokers.Where(b => brokersIds.Contains(b.Id)).ToList();
            //}

            return _unitOfWork.Brokers.GetAll().ToList();
        }

        public void AddBroker(Broker broker)
        {
            _unitOfWork.Brokers.Add(broker);
            _unitOfWork.Save();
        }

        public void UpdateBroker(Broker broker)
        {
            _unitOfWork.Brokers.Update(broker);
            _unitOfWork.Save();
        }

        public void DeleteBroker(int id)
        {
            _unitOfWork.Brokers.Remove(id);
            _unitOfWork.Save();
        }
    }
}
