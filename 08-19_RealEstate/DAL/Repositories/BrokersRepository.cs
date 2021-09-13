using _08_19_RealEstate.DAL.IRepositories;
using _08_19_RealEstate.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace _08_19_RealEstate.DAL.Repositories
{
    public class BrokersRepository : Repository<Broker>, IBrokersRepository
    {
        private readonly DbSet<Broker> _dbSet;

        public BrokersRepository(DataContext context)
            : base(context)
        {
            _dbSet = context.Brokers;
        }

        public override IEnumerable<Broker> GetAll()
        {
            return _dbSet.Include(b => b.Companies);
        }
    }
}
