using _08_19_RealEstate.Services;
using _08_19_RealEstate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _08_19_RealEstate.Controllers
{
    public class CompaniesController : Controller
    {
        private CompaniesService _companiesService;
        private GeneralService _generalService;

        public CompaniesController(CompaniesService dbService, GeneralService generalService)
        {
            _companiesService = dbService;
            _generalService = generalService;
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
        public IActionResult Create(CompanyFormViewModel model)
        {
            _companiesService.AddCompanyWithBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            CompanyFormViewModel model = _generalService.GetModelForEditingCompany(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CompanyFormViewModel model)
        {
            _companiesService.UpdateCompanyWithBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int companyId, int addressId)
        {
            if (!_companiesService.Delete(companyId))
                return Json("The company could not be deleted.");

            return Json(null);
        }
    }
}
