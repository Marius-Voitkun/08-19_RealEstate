using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class BrokersService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrokersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Broker> GetAll()
        {
            return _unitOfWork.Brokers.GetAll().ToList();
        }

        public Broker Get(int id)
        {
            return _unitOfWork.Brokers.Get(id);
        }

        public List<Broker> GetBrokersInCompany(int companyId)
        {
            return _unitOfWork.Brokers.Find(b => b.Companies
                                                    .Select(c => c.Id)
                                                    .Contains(companyId))
                                                .ToList();
        }

        public void Add(Broker broker)
        {
            _unitOfWork.Brokers.Add(broker);
            _unitOfWork.Save();
        }

        public void Update(Broker broker)
        {
            _unitOfWork.Brokers.Update(broker);
            _unitOfWork.Save();
        }

        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Brokers.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
