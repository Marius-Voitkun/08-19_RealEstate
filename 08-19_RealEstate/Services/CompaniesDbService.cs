using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using _08_19_RealEstate.ViewModels;
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

        public int AddCompanyAndGetId(CompanyFormViewModel model)
        {
            model.Company.Brokers = _context.Brokers.Where(b => model.SelectedBrokersIds.Contains(b.Id)).ToList();
            _context.Companies.Add(model.Company);
            _context.SaveChanges();

            return model.Company.Id;
        }

        public void UpdateCompany(CompanyFormViewModel model)
        {
            model.Company.Brokers = _context.Brokers.Where(b => model.SelectedBrokersIds.Contains(b.Id)).ToList();
            _context.Companies.Update(model.Company);
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
