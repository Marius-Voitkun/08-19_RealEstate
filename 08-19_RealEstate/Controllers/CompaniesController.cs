using _08_19_RealEstate.Services;
using _08_19_RealEstate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _08_19_RealEstate.Controllers
{
    public class CompaniesController : Controller
    {
        private GeneralService _generalService;
        private CompaniesService _companiesService;
        private BrokersService _brokersService;

        public CompaniesController(GeneralService generalService, CompaniesService companiesService, BrokersService brokersService)
        {
            _generalService = generalService;
            _companiesService = companiesService;
            _brokersService = brokersService;
        }

        public IActionResult Index()
        {
            return View(_companiesService.GetAll());
        }

        public IActionResult Create()
        {
            CompanyFormViewModel model = _generalService.GetModelForCreatingCompany();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompanyFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Brokers = _brokersService.GetAll();
                return View(model);
            }

            _companiesService.AddCompanyWithBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            CompanyFormViewModel model = _generalService.GetModelForEditingCompany(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompanyFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Brokers = _brokersService.GetAll();
                return View(model);
            }

            _companiesService.UpdateCompanyWithBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int companyId, int addressId)
        {
            if (!_companiesService.Delete(companyId, addressId))
                return Json("The company could not be deleted.");

            return Json(null);
        }
    }
}
