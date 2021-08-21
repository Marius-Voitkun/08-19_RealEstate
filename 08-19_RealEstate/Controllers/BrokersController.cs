﻿using _08_19_RealEstate.Models;
using _08_19_RealEstate.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _08_19_RealEstate.Controllers
{
    public class BrokersController : Controller
    {
        private BrokersDbService _dbService;

        public BrokersController(BrokersDbService dbService)
        {
            _dbService = dbService;
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
    }
}