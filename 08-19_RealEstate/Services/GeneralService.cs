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

        public ApartmentFormViewModel GetModelForCreatingApartment()
        {
            return new()
            {
                Apartment = new() { Address = new() },
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

        public void AddNewCompanyWithBrokers(CompanyFormViewModel model)
        {
            int companyId = _companiesDbService.AddCompanyAndGetItsId(model.Company);
            _companiesBrokersDbService.AddCompaniesBrokersJunctions(companyId, model.SelectedBrokersIds);
        }

        public List<Broker> GetBrokersInCompany(int companyId)
        {
            List<int> brokersIds = _companiesBrokersDbService.GetBrokersIdsForCompany(companyId);
            List<Broker> brokers = _brokersDbService.GetBrokers(brokersIds);

            return brokers;
        }
    }
}
