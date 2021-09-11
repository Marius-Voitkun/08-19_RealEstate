using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class GeneralService
    {
        private DataContext _context;
        private readonly ApartmentsService _apartmentsService;
        private readonly BrokersService _brokersService;
        private readonly CompaniesService _companiesService;
        private readonly AddressesService _addressesService;
        private readonly CompaniesBrokersService _companiesBrokersService;

        public GeneralService(DataContext context, ApartmentsService apartmentsService, BrokersService brokersService, CompaniesService companiesService,
                    AddressesService addressesService, CompaniesBrokersService companiesBrokersService)
        {
            _context = context;
            _apartmentsService = apartmentsService;
            _brokersService = brokersService;
            _companiesService = companiesService;
            _addressesService = addressesService;
            _companiesBrokersService = companiesBrokersService;
        }

        public ApartmentsIndexViewModel GetModelForApartmentsIndex(ApartmentsFilterModel filterModel)
        {
            ApartmentsIndexViewModel model = new()
            {
                Apartments = _apartmentsService.GetApartments(filterModel),
                Companies = _companiesService.GetAll(),
                Brokers = _brokersService.GetAll(),
            };

            //model.Cities = _addressesDbService.GetCities();
            model.Cities = _apartmentsService.GetApartments(new ApartmentsFilterModel()).Select(a => a.Address.City).Distinct().ToList();

            return model;
        }

        public ApartmentsOfBrokerViewModel GetApartmentsOfBroker(ApartmentsOfBrokerViewModel modelForFiltering)
        {
            int? brokerId = modelForFiltering.FilterModel.BrokerId;

            ApartmentsOfBrokerViewModel model = new()
            {
                Apartments = _apartmentsService.GetApartments(modelForFiltering.FilterModel),
                FilterModel = new()
            };

            model.Cities = _apartmentsService.GetApartments(new ApartmentsFilterModel() { BrokerId = brokerId })
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
                CompaniesBrokersJson = JsonConvert.SerializeObject(_companiesBrokersService.GetJunctions())
            };

            model.BrokersJson = JsonConvert.SerializeObject(model.Brokers);

            return model;
        }

        public ApartmentFormViewModel GetModelForEditingApartment(int id)
        {
            ApartmentFormViewModel model = new()
            {
                Apartment = _apartmentsService.GetApartments(new ApartmentsFilterModel { ApartmentId = id })[0],
                Brokers = _brokersService.GetAll(),
                Companies = _companiesService.GetAll(),
                CompaniesBrokersJson = JsonConvert.SerializeObject(_companiesBrokersService.GetJunctions())
            };

            model.BrokersJson = JsonConvert.SerializeObject(model.Brokers);

            return model;
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

        //public void AddNewCompanyWithItsBrokers(CompanyFormViewModel model)
        //{
        //    int companyId = _companiesService.AddCompanyWithBrokers(model);
        //    //_companiesBrokersDbService.AddJunctions(companyId, model.SelectedBrokersIds);
        //}

        //public void UpdateCompanyWithItsBrokers(CompanyFormViewModel model)
        //{
        //    _companiesService.UpdateCompany(model);

        //    //_companiesBrokersDbService.DeleteJunctionsByCompany(model.Company.Id);
        //    //_companiesBrokersDbService.AddJunctions(model.Company.Id, model.SelectedBrokersIds);
        //}

        public List<Broker> GetBrokersInCompany(int companyId)
        {
            List<int> brokersIds = _companiesBrokersService.GetBrokersIdsForCompany(companyId);
            List<Broker> brokers = _brokersService.GetAll(brokersIds);

            return brokers;
        }

        //public bool DeleteBroker(int id)
        //{
        //    try
        //    {
        //        _brokersService.Delete(id);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public bool DeleteCompany(int companyId, int addressId)
        //{
        //    try
        //    {
        //        //_companiesBrokersDbService.DeleteJunctionsByCompany(companyId);
        //        _companiesService.Delete(companyId);
        //        _addressesService.DeleteAddress(addressId);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
