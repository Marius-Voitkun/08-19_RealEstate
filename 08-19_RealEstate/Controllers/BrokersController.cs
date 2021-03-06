using _08_19_RealEstate.Models;
using _08_19_RealEstate.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace _08_19_RealEstate.Controllers
{
    public class BrokersController : Controller
    {
        private BrokersDbService _dbService;
        private GeneralService _generalService;

        public BrokersController(BrokersDbService dbService, GeneralService generalService)
        {
            _dbService = dbService;
            _generalService = generalService;
        }

        public IActionResult Index()
        {
            return View(_dbService.GetBrokers());
        }

        public IActionResult Create()
        {
            return View(new Broker());
        }

        [HttpPost]
        public IActionResult Create(Broker newBroker)
        {
            _dbService.AddBroker(newBroker);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Broker broker = _dbService.GetBrokers().SingleOrDefault(b => b.Id == id);

            if (broker == null)
                return NotFound();

            return View(broker);
        }

        [HttpPost]
        public IActionResult Edit(Broker broker)
        {
            _dbService.UpdateBroker(broker);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!_generalService.DeleteBroker(id))
                return Json("The broker could not be deleted.");

            return Json(null);
        }

        public IActionResult BrokersInCompany(int companyId, string companyName)
        {
            List<Broker> brokers = _generalService.GetBrokersInCompany(companyId);

            ViewData["CompanyName"] = companyName;

            return View(brokers);
        }
    }
}
