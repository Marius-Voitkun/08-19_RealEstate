using _08_19_RealEstate.Data;
using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class CompaniesDbService
    {
        private DataContext _context;

        public CompaniesDbService(DataContext context)
        {
            _context = context;
        }

        public List<Company> GetCompanies()
        {
            return _context.Companies.Include(c => c.Address).Include(c => c.Brokers).ToList();
        }

        public int AddCompanyAndGetId(Company company)
        {
            _context.Companies.Add(company);
            _context.SaveChanges();

            return company.Id;
        }

        public void UpdateCompany(Company company)
        {
            //_addressesDbService.UpdateAddress(company.Address);

            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        public void DeleteCompany(int id)
        {
            Company company = _context.Companies.Single(c => c.Id == id);

            _context.Companies.Remove(company);
            _context.SaveChanges();
        }
    }
}
