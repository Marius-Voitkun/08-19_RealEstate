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

        public IActionResult Index()
        {
            return View(_dbService.GetApartments());
        }

        public IActionResult Create()
        {
            ApartmentFormViewModel model = _generalService.GetModelForApartmentForm();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Apartment apartment)
        {
            _dbService.AddApartment(apartment);

            return RedirectToAction("Index");
        }
    }
}
