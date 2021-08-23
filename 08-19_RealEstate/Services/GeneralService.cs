using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;

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

        public ApartmentFormViewModel GetModelForApartmentForm()
        {
            ApartmentFormViewModel model = new()
            {
                Apartment = new() { Address = new() },
                Brokers = _brokersDbService.GetBrokers(),
                Companies = _companiesDbService.GetCompanies()
            };

            return model;
        }

        public CompanyFormViewModel GetModelForCompanyForm()
        {
            CompanyFormViewModel model = new()
            {
                Company = new() { Address = new() },
                Brokers = _brokersDbService.GetBrokers()
            };

            return model;
        }

        public void AddNewCompanyWithBrokers(CompanyFormViewModel model)
        {
            int companyId = _companiesDbService.AddCompanyAndGetItsId(model.Company);
            _companiesBrokersDbService.AddCompaniesBrokersJunctions(companyId, model.SelectedBrokersIds);
        }
    }
}
