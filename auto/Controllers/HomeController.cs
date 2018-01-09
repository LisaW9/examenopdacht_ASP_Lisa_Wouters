using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using auto.Models;
using auto.Services;

namespace auto.Controllers
{
    public class HomeController : Controller
    {
        private IModelFetcher _modelFetcher;

        public HomeController(IModelFetcher carModelFetcher)
        {
            _modelFetcher = carModelFetcher;
        }

        public IActionResult Index()
        {
            var model = new CarIndexViewModel
            {
                Cars = _modelFetcher.GetAllCarViewModels()
            };

            return View(model);
        }

        public IActionResult Owners()
        {
            var model = new OwnerIndexViewModel
            {
                Owners = _modelFetcher.GetAllOwnerViewModels()
            };

            return View(model);
        }

        public IActionResult Types()
        {
            var model = new TypeIndexViewModel
            {
                Types = _modelFetcher.GetAllTypeViewModels()
            };

            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Edit([FromRoute] int id)
        {
            var data = _modelFetcher.GetCarViewModel(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        public IActionResult New()
        {
            var car = new CarViewModel();
            var owners = _modelFetcher.GetAllOwners().Select(_modelFetcher.ConvertOwnersToSelectListItem).ToList();
            var types = _modelFetcher.GetAllTypes().Select(_modelFetcher.ConvertTypesToSelectListItem).ToList();
            car.Owners = owners;
            car.Types = types;
            return View("Edit", car);
        }

        [HttpPost]
        public IActionResult Save(CarViewModel car)
        {
            car.Id = _modelFetcher.Save(car);
            return Redirect("../");
        }

        [HttpPost]
        public IActionResult Delete(CarViewModel car)
        {
            _modelFetcher.Delete(car);
            return Redirect("/");
        }
    }
}
