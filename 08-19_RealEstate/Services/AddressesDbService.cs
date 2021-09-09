using _08_19_RealEstate.Data;
using _08_19_RealEstate.Models;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class AddressesDbService
    {
        private DataContext _context;

        public AddressesDbService(DataContext context)
        {
            _context = context;
        }

        public List<Address> GetAddresses()
        {
            return _context.Addresses.ToList();
        }

        public List<string> GetCities()
        {
            return _context.Addresses.Select(a => a.City).Distinct().ToList();
        }

        public int AddAddressAndGetItsId(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();

            return address.Id;
        }

        public void UpdateAddress(Address address)
        {
            _context.Update(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
            Address address = _context.Addresses.Single(a => a.Id == id);

            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }
    }
}
