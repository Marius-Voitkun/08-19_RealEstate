using _08_19_RealEstate.DAL.Repositories;
using _08_19_RealEstate.Models;
using System;

namespace _08_19_RealEstate.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IApartmentsRepository Apartments { get; }
        IBrokersRepository Brokers { get; }
        ICompaniesRepository Companies { get; }
        IRepository<Address> Addresses { get; }
        int Save();
    }
}
