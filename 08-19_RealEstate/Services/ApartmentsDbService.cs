﻿using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.Services
{
    public class ApartmentsDbService
    {
        private readonly SqlConnection _connection;
        private readonly AddressesDbService _addressesDbService;
        private readonly BrokersDbService _brokersDbService;
        private readonly CompaniesDbService _companiesDbService;

        public ApartmentsDbService(SqlConnection connection, AddressesDbService addressesDbService, BrokersDbService brokersDbService, CompaniesDbService companiesDbService)
        {
            _connection = connection;
            _addressesDbService = addressesDbService;
            _brokersDbService = brokersDbService;
            _companiesDbService = companiesDbService;
        }

        public List<Apartment> GetApartments(ApartmentsIndexViewModel modelForFiltering)
        {
            List<Apartment> apartments = new();

            string query = GenerateQueryToGetFilteredApartments(modelForFiltering);

            using (_connection)
            {
                apartments = _connection.Query<Apartment>(query).ToList();
            }

            List<Address> addresses = _addressesDbService.GetAddresses();
            List<Broker> brokers = _brokersDbService.GetBrokers();
            List<Company> companies = _companiesDbService.GetCompanies();

            foreach (var apartment in apartments)
            {
                apartment.Address = addresses.Single(a => a.Id == apartment.AddressId);
                apartment.Broker = brokers.Single(b => b.Id == apartment.BrokerId);
                apartment.Company = companies.Single(c => c.Id == apartment.CompanyId);
            }

            return apartments;
        }

        private string GenerateQueryToGetFilteredApartments(ApartmentsIndexViewModel modelForFiltering)
        {
            string fragmentForCity = modelForFiltering.CityForFiltering != null
                                                ? $"City = N'{modelForFiltering.CityForFiltering}'"
                                                : null;

            string fragmentForCompany = modelForFiltering.CompanyIdForFiltering != null
                                                ? $"CompanyId = {modelForFiltering.CompanyIdForFiltering}"
                                                : null;

            string fragmentForBroker = modelForFiltering.BrokerIdForFiltering != null
                                                ? $"BrokerId = {modelForFiltering.BrokerIdForFiltering}"
                                                : null;

            List<string> fragments = new() { fragmentForCity, fragmentForCompany, fragmentForBroker };
            
            string joinedFragments = string.Join(" AND ", fragments.Where(f => !string.IsNullOrWhiteSpace(f)));

            string whereClause = !string.IsNullOrWhiteSpace(joinedFragments)
                                                ? "WHERE " + joinedFragments
                                                : "";

            string query = @$"
SELECT ap.Id, ap.AddressId, ap.Floor, ap.TotalFloorsInBuilding, ap.AreaInSqm, ap.BrokerId, ap.CompanyId
FROM dbo.Apartments ap
JOIN dbo.Addresses ad ON ad.Id = ap.AddressId
{whereClause};";

            return query;
        }

        public void AddApartment(Apartment apartment)
        {
            int addressId = _addressesDbService.AddAddressAndGetItsId(apartment.Address);

            string query = $@"INSERT INTO dbo.Apartments (AddressId, Floor, TotalFloorsInBuilding, AreaInSqm, BrokerId, CompanyId)
                              VALUES ({addressId}, {apartment.Floor}, {apartment.TotalFloorsInBuilding},
                                    {apartment.AreaInSqm}, {apartment.BrokerId}, {apartment.CompanyId});";

            using (_connection)
            {
                _connection.Execute(query);
            }
        }
    }
}
