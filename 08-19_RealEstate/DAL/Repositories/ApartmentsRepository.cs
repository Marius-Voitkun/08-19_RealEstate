using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _08_19_RealEstate.DAL.Repositories
{
    public class ApartmentsRepository : Repository<Apartment>, IApartmentsRepository
    {
        private readonly DbSet<Apartment> _dbSet;

        public ApartmentsRepository(DataContext context)
            : base(context)
        {
            _dbSet = context.Apartments;
        }

        public override IEnumerable<Apartment> GetAll()
        {
            return _dbSet
                    .Include(a => a.Address)
                    .Include(a => a.Broker)
                    .Include(a => a.Company);
        }

        public override Apartment Get(int id)
        {
            return _dbSet
                    .Include(a => a.Address)
                    .Include(a => a.Broker)
                    .Include(a => a.Company)
                    .SingleOrDefault(a => a.Id == id);
        }

        public override IEnumerable<Apartment> Find(Expression<Func<Apartment, bool>> predicate)
        {
            return _dbSet
                    .Include(a => a.Address)
                    .Include(a => a.Broker)
                    .Include(a => a.Company)
                    .Where(predicate);
        }
    }
}
