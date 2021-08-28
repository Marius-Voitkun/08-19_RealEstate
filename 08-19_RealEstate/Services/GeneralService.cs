using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class GeneralService
    {
        private readonly ApartmentsDbService _apartmentsDbService;
        private readonly BrokersDbService _brokersDbService;
        private readonly CompaniesDbService _companiesDbService;
        private readonly AddressesDbService _addressesDbService;
        private readonly CompaniesBrokersDbService _companiesBrokersDbService;

        public GeneralService(ApartmentsDbService apartmentsDbService, BrokersDbService brokersDbService, CompaniesDbService companiesDbService,
                    AddressesDbService addressesDbService, CompaniesBrokersDbService companiesBrokersDbService)
        {
            _apartmentsDbService = apartmentsDbService;
            _brokersDbService = brokersDbService;
            _companiesDbService = companiesDbService;
            _addressesDbService = addressesDbService;
            _companiesBrokersDbService = companiesBrokersDbService;
        }

        public ApartmentsIndexViewModel GetModelForApartmentsIndex(ApartmentsFilterModel filterModel)
        {
            ApartmentsIndexViewModel model = new()
            {
                Apartments = _apartmentsDbService.GetApartments(filterModel),
                Companies = _companiesDbService.GetCompanies(),
                Brokers = _brokersDbService.GetBrokers(),
            };

            //model.Cities = _addressesDbService.GetCities();
            model.Cities = _apartmentsDbService.GetApartments(new ApartmentsFilterModel()).Select(a => a.Address.City).Distinct().ToList();

            return model;
        }

        public ApartmentsOfBrokerViewModel GetApartmentsOfBroker(ApartmentsOfBrokerViewModel modelForFiltering)
        {
            int? brokerId = modelForFiltering.FilterModel.BrokerId;

            ApartmentsOfBrokerViewModel model = new()
            {
                Apartments = _apartmentsDbService.GetApartments(modelForFiltering.FilterModel),
                FilterModel = new()
            };

            model.Cities = _apartmentsDbService.GetApartments(new ApartmentsFilterModel() { BrokerId = brokerId })
                                .Select(a => a.Address.City).Distinct().ToList();

            return model;
        }

        public ApartmentFormViewModel GetModelForCreatingApartment()
        {
            return new()
            {
                Apartment = new() { Address = new() },
                Brokers = _brokersDbService.GetBrokers(),
                Companies = _companiesDbService.GetCompanies()
            };
        }

        public ApartmentFormViewModel GetModelForEditingApartment(int id)
        {
            return new()
            {
                Apartment = _apartmentsDbService.GetApartments(new ApartmentsFilterModel { ApartmentId = id })[0],
                Brokers = _brokersDbService.GetBrokers(),
                Companies = _companiesDbService.GetCompanies()
            };
        }

        public CompanyFormViewModel GetModelForCreatingCompany()
        {
            return new()
            {
                Company = new() { Address = new() },
                Brokers = _brokersDbService.GetBrokers()
            };
        }

        public CompanyFormViewModel GetModelForEditingCompany(int id)
        {
            return new()
            {
                Company = _companiesDbService.GetCompanies().SingleOrDefault(c => c.Id == id),
                Brokers = _brokersDbService.GetBrokers(),
                SelectedBrokersIds = GetBrokersInCompany(id).Select(b => b.Id).ToList()
            };
        }

        public void AddNewCompanyWithItsBrokers(CompanyFormViewModel model)
        {
            int companyId = _companiesDbService.AddCompanyAndGetId(model.Company);
            _companiesBrokersDbService.AddCompaniesBrokersJunctions(companyId, model.SelectedBrokersIds);
        }

        public void UpdateCompanyWithItsBrokers(CompanyFormViewModel model)
        {
            _companiesDbService.UpdateCompany(model.Company);

            _companiesBrokersDbService.DeleteCompaniesBrokersJunctions(model.Company.Id);
            _companiesBrokersDbService.AddCompaniesBrokersJunctions(model.Company.Id, model.SelectedBrokersIds);
        }

        public List<Broker> GetBrokersInCompany(int companyId)
        {
            List<int> brokersIds = _companiesBrokersDbService.GetBrokersIdsForCompany(companyId);
            List<Broker> brokers = _brokersDbService.GetBrokers(brokersIds);

            return brokers;
        }
    }
}
