using _08_19_RealEstate.Models;

namespace _08_19_RealEstate.DAL.Repositories
{
    public class BrokersRepository : Repository<Broker>, IBrokersRepository
    {
        public BrokersRepository(DataContext context)
            : base(context)
        {
        }

        public DataContext DataContext
        {
            get { return Context as DataContext; }
        }
    }
}
