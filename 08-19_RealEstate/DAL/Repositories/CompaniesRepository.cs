using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.DAL.Repositories
{
    public class CompaniesRepository : Repository<Company>, ICompaniesRepository
    {
        private readonly DbSet<Company> _dbSet;

        public CompaniesRepository(DataContext context)
            : base(context)
        {
            _dbSet = context.Companies;
        }

        public override IEnumerable<Company> GetAll()
        {
            return _dbSet.Include(c => c.Address).Include(c => c.Brokers);
        }

        public override Company Get(int id)
        {
            return _dbSet.Include(c => c.Address).Include(c => c.Brokers).SingleOrDefault(c => c.Id == id);
        }
    }
}
