using _08_19_RealEstate.DAL.Repositories;

namespace _08_19_RealEstate.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public IBrokersRepository Brokers { get; private set; }
        public ICompaniesRepository Companies { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Brokers = new BrokersRepository(_context);
            Companies = new CompaniesRepository(_context);
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
