using _08_19_RealEstate.Services;
using _08_19_RealEstate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _08_19_RealEstate.Controllers
{
    public class CompaniesController : Controller
    {
        private CompaniesDbService _dbService;
        private GeneralService _generalService;

        public CompaniesController(CompaniesDbService dbService, GeneralService generalService)
        {
            _dbService = dbService;
            _generalService = generalService;
        }

        public IActionResult Index()
        {
            return View(_dbService.GetCompanies());
        }

        public IActionResult Create()
        {
            CompanyFormViewModel model = _generalService.GetModelForCreatingCompany();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CompanyFormViewModel model)
        {
            _generalService.AddNewCompanyWithItsBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            CompanyFormViewModel model = _generalService.GetModelForEditingCompany(id);

            if (model.Company == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CompanyFormViewModel model)
        {
            _generalService.UpdateCompanyWithItsBrokers(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int companyId, int addressId)
        {
            if (!_generalService.DeleteCompany(companyId, addressId))
                return Json("The company could not be deleted.");

            return Json(null);
        }
    }
}
