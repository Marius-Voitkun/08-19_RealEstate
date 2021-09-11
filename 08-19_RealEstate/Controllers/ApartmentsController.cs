using _08_19_RealEstate.Models;
using _08_19_RealEstate.Services;
using _08_19_RealEstate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _08_19_RealEstate.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentsService _apartmentsService;
        private GeneralService _generalService;

        public ApartmentsController(ApartmentsService apartmentsService, GeneralService generalService)
        {
            _apartmentsService = apartmentsService;
            _generalService = generalService;
        }

        public IActionResult Index(ApartmentsIndexViewModel modelForFiltering)
        {
            if (modelForFiltering.FilterModel == null)
                modelForFiltering.FilterModel = new();

            return View(_generalService.GetModelForApartmentsIndex(modelForFiltering.FilterModel));
        }

        public IActionResult Details(int id)
        {
            return View(_apartmentsService.Get(id));
        }

        public IActionResult Create()
        {
            return View(_generalService.GetModelForCreatingApartment());
        }

        [HttpPost]
        public IActionResult Create(Apartment apartment)
        {
            _apartmentsService.Add(apartment);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_generalService.GetModelForEditingApartment(id));
        }

        [HttpPost]
        public IActionResult Edit(Apartment apartment)
        {
            _apartmentsService.Update(apartment);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int apartmentId, int addressId)
        {
            _apartmentsService.Delete(apartmentId, addressId);

            return RedirectToAction("Index");
        }

        public IActionResult ApartmentsOfBroker(int brokerId, string brokerName, ApartmentsOfBrokerViewModel modelForFiltering)
        {
            if (modelForFiltering.FilterModel == null)
                modelForFiltering.FilterModel = new();

            modelForFiltering.FilterModel.BrokerId = brokerId;

            ViewData["BrokerId"] = brokerId;
            ViewData["BrokerName"] = brokerName;

            return View(_generalService.GetApartmentsOfBroker(modelForFiltering));
        }
    }
}
