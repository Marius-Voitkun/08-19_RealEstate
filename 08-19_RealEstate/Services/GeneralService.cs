using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class GeneralService
    {
        private readonly ApartmentsService _apartmentsService;
        private readonly BrokersService _brokersService;
        private readonly CompaniesService _companiesService;

        public GeneralService(ApartmentsService apartmentsService, BrokersService brokersService, CompaniesService companiesService)
        {
            _apartmentsService = apartmentsService;
            _brokersService = brokersService;
            _companiesService = companiesService;
        }

        public ApartmentsIndexViewModel GetModelForApartmentsIndex(ApartmentsFilterModel filterModel)
        {
            ApartmentsIndexViewModel model = new()
            {
                Apartments = _apartmentsService.GetAllOrFiltered(filterModel),
                Companies = _companiesService.GetAll(),
                Brokers = _brokersService.GetAll(),
            };

            model.Cities = _apartmentsService.GetAllOrFiltered(new ApartmentsFilterModel()).Select(a => a.Address.City).Distinct().ToList();

            return model;
        }

        public ApartmentsOfBrokerViewModel GetApartmentsOfBroker(ApartmentsOfBrokerViewModel modelForFiltering)
        {
            int? brokerId = modelForFiltering.FilterModel.BrokerId;

            ApartmentsOfBrokerViewModel model = new()
            {
                Apartments = _apartmentsService.GetAllOrFiltered(modelForFiltering.FilterModel),
                FilterModel = new()
            };

            model.Cities = _apartmentsService.GetAllOrFiltered(new ApartmentsFilterModel() { BrokerId = brokerId })
                                .Select(a => a.Address.City).Distinct().ToList();

            return model;
        }

        public ApartmentFormViewModel GetModelForCreatingApartment()
        {
            ApartmentFormViewModel model = new()
            {
                Apartment = new() { Address = new() },
                Brokers = _brokersService.GetAll(),
                Companies = _companiesService.GetAll(),
            };

            model.CompaniesBrokersJson = JsonConvert.SerializeObject(GetCompaniesBrokersJunctions(model.Companies));
            model.BrokersJson = JsonConvert.SerializeObject(model.Brokers);

            return model;
        }

        public ApartmentFormViewModel GetModelForEditingApartment(int id)
        {
            ApartmentFormViewModel model = new()
            {
                Apartment = _apartmentsService.GetAllOrFiltered(new ApartmentsFilterModel { ApartmentId = id })[0],
                Brokers = _brokersService.GetAll(),
                Companies = _companiesService.GetAll(),
            };

            model.CompaniesBrokersJson = JsonConvert.SerializeObject(GetCompaniesBrokersJunctions(model.Companies));
            model.BrokersJson = JsonConvert.SerializeObject(model.Brokers);

            return model;
        }

        private List<CompanyBrokerJunction> GetCompaniesBrokersJunctions(List<Company> companies)
        {
            List<CompanyBrokerJunction> companiesBrokers = new();

            foreach (var company in companies)
            {
                foreach (var broker in company.Brokers)
                {
                    companiesBrokers.Add(new CompanyBrokerJunction
                    {
                        CompanyId = company.Id,
                        BrokerId = broker.Id
                    });
                }
            }

            return companiesBrokers;
        }

        public CompanyFormViewModel GetModelForCreatingCompany()
        {
            return new()
            {
                Company = new() { Address = new() },
                Brokers = _brokersService.GetAll()
            };
        }

        public CompanyFormViewModel GetModelForEditingCompany(int id)
        {
            CompanyFormViewModel model = new()
            {
                Company = _companiesService.Get(id),
                Brokers = _brokersService.GetAll()
            };

            model.SelectedBrokersIds = model.Company.Brokers.Select(b => b.Id).ToList();

            return model;
        }
    }
}
