using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class ApartmentsService
    {
        private DataContext _context;

        public ApartmentsService(DataContext context)
        {
            _context = context;
        }

        public List<Apartment> GetApartments(ApartmentsFilterModel filterModel)
        {
            List<Apartment> apartments = _context.Apartments
                                                    .Include(a => a.Address)
                                                    .Include(a => a.Broker)
                                                    .Include(a => a.Company)
                                                    .ToList();

            if (filterModel.ApartmentId != null && filterModel.ApartmentId != 0)
                apartments = apartments.Where(a => a.Id == filterModel.ApartmentId).ToList();

            if (filterModel.CompanyId != null && filterModel.CompanyId != 0)
                apartments = apartments.Where(a => a.CompanyId == filterModel.CompanyId).ToList();

            if (filterModel.BrokerId != null && filterModel.BrokerId != 0)
                apartments = apartments.Where(a => a.BrokerId == filterModel.BrokerId).ToList();

            if (!string.IsNullOrWhiteSpace(filterModel.City))
                apartments = apartments.Where(a => a.Address.City == filterModel.City).ToList();

            return apartments;
        }

        public void AddApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            _context.SaveChanges();
        }

        public void UpdateApartment(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            _context.SaveChanges();
        }

        public void DeleteApartment(int apartmentId, int addressId)
        {
            Apartment apartment = _context.Apartments.Single(a => a.Id == apartmentId);

            _context.Apartments.Remove(apartment);
            _context.SaveChanges();
        }
    }
}
