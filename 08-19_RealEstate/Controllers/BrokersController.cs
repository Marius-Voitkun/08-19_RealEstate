using _08_19_RealEstate.Models;
using _08_19_RealEstate.Services;
using Microsoft.AspNetCore.Mvc;

namespace _08_19_RealEstate.Controllers
{
    public class BrokersController : Controller
    {
        private BrokersService _brokersService;

        public BrokersController(BrokersService brokersService)
        {
            _brokersService = brokersService;
        }

        public IActionResult Index()
        {
            return View(_brokersService.GetAll());
        }

        public IActionResult Create()
        {
            return View(new Broker());
        }

        [HttpPost]
        public IActionResult Create(Broker broker)
        {
            _brokersService.Add(broker);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(_brokersService.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(Broker broker)
        {
            _brokersService.Update(broker);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!_brokersService.Delete(id))
                return Json("The broker could not be deleted.");

            return Json(null);
        }

        public IActionResult BrokersInCompany(int companyId, string companyName)
        {
            ViewData["CompanyName"] = companyName;

            return View(_brokersService.GetBrokersInCompany(companyId));
        }
    }
}
