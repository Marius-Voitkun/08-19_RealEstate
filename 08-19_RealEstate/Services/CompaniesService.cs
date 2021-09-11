using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompaniesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Company> GetAll()
        {
            return _unitOfWork.Companies.GetAll().ToList();
        }

        public Company Get(int id)
        {
            return _unitOfWork.Companies.Get(id);
        }


        public void AddCompanyWithBrokers(CompanyFormViewModel model)
        {
            model.Company.Brokers = _unitOfWork.Brokers.Find(b => model.SelectedBrokersIds.Contains(b.Id)).ToList();
            _unitOfWork.Companies.Add(model.Company);
            _unitOfWork.Save();
        }

        public void UpdateCompanyWithBrokers(CompanyFormViewModel model)
        {
            _unitOfWork.Companies.Update(model.Company);
            _unitOfWork.Companies.Get(model.Company.Id).Brokers = _unitOfWork.Brokers.Find(b => model.SelectedBrokersIds.Contains(b.Id)).ToList();

            _unitOfWork.Save();
        }

        public bool Delete(int id)
        {
            try
            {
                _unitOfWork.Companies.Delete(id);
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
