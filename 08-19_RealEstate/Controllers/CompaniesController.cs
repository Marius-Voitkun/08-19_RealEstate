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
            CompanyFormViewModel model = _generalService.GetModelForCompanyForm();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CompanyFormViewModel model)
        {
            _generalService.AddNewCompanyWithBrokers(model);

            return RedirectToAction("Index");
        }
    }
}
