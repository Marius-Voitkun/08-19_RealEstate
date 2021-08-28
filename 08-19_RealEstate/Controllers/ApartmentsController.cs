using _08_19_RealEstate.Models;
using _08_19_RealEstate.Services;
using _08_19_RealEstate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.Controllers
{
    public class ApartmentsController : Controller
    {
        private ApartmentsDbService _dbService;
        private GeneralService _generalService;

        public ApartmentsController(ApartmentsDbService dbService, GeneralService generalService)
        {
            _dbService = dbService;
            _generalService = generalService;
        }

        public IActionResult Index(ApartmentsIndexViewModel modelForFiltering)
        {
            if (modelForFiltering.FilterModel == null)
                modelForFiltering.FilterModel = new();

            return View(_generalService.GetModelForApartmentsIndex(modelForFiltering.FilterModel));
        }

        public IActionResult Create()
        {
            ApartmentFormViewModel model = _generalService.GetModelForCreatingApartment();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Apartment apartment)
        {
            _dbService.AddApartment(apartment);

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
