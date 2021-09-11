using _08_19_RealEstate.DAL;
using _08_19_RealEstate.Models;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Services
{
    public class ApartmentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApartmentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Apartment> GetAllOrFiltered(ApartmentsFilterModel filterModel)
        {
            List<Apartment> apartments = _unitOfWork.Apartments.GetAll().ToList();

            if (filterModel.ApartmentId != null && filterModel.ApartmentId != 0)
                apartments = apartments.Where(a => a.Id == filterModel.ApartmentId).ToList();

            if (filterModel.CompanyId != null && filterModel.CompanyId != 0)
                apartments = apartments.Where(a => a.CompanyId == filterModel.CompanyId).ToList();

            if (filterModel.BrokerId != null && filterModel.BrokerId != 0)
                apartments = apartments.Where(a => a.BrokerId == filterModel.BrokerId).ToList();

            if (!string.IsNullOrWhiteSpace(filterModel.City))
                apartments = apartments.Where(a => a.Address.City == filterModel.City).ToList();

            return apartments;
        }

        public Apartment Get(int id)
        {
            return _unitOfWork.Apartments.Get(id);
        }

        public void Add(Apartment apartment)
        {
            _unitOfWork.Apartments.Add(apartment);
            _unitOfWork.Save();
        }

        public void Update(Apartment apartment)
        {
            _unitOfWork.Apartments.Update(apartment);
            _unitOfWork.Save();
        }

        public void Delete(int apartmentId, int addressId)
        {
            _unitOfWork.Apartments.Delete(apartmentId);
            _unitOfWork.Addresses.Delete(addressId);
            _unitOfWork.Save();
        }
    }
}
