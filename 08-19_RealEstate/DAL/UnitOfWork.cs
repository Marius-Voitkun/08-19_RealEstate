using _08_19_RealEstate.DAL.Repositories;
using _08_19_RealEstate.Models;

namespace _08_19_RealEstate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IApartmentsRepository Apartments { get; private set; }
        public IBrokersRepository Brokers { get; private set; }
        public ICompaniesRepository Companies { get; private set; }
        public IRepository<Address> Addresses { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Apartments = new ApartmentsRepository(_context);
            Brokers = new BrokersRepository(_context);
            Companies = new CompaniesRepository(_context);
            Addresses = new Repository<Address>(_context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
