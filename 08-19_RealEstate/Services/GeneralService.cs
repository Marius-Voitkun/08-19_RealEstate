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

        public GeneralService(ApartmentsDbService apartmentsDbService, BrokersDbService brokersDbService, CompaniesDbService companiesDbService, AddressesDbService addressesDbService)
        {
            _apartmentsDbService = apartmentsDbService;
            _brokersDbService = brokersDbService;
            _companiesDbService = companiesDbService;
            _addressesDbService = addressesDbService;
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
    }
}
