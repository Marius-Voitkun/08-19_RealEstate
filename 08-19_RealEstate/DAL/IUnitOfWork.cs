using _08_19_RealEstate.DAL.Repositories;
using System;

namespace _08_19_RealEstate.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IBrokersRepository Brokers { get; }
        ICompaniesRepository Companies { get; }
        int Save();
    }
}
