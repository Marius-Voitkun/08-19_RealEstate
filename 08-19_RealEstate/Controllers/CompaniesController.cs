using _08_19_RealEstate.Services;
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

        public CompaniesController(CompaniesDbService dbService)
        {
            _dbService = dbService;
        }

        public IActionResult Index()
        {
            return View(_dbService.GetCompanies());
        }
    }
}
