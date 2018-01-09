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
                // Indien er geen person met dat id is, geven we een 404 terug. Je kan dit zien door te hoveren over de NotFound methode.
                // Je hebt nog massa's andere: Accepted(), Ok(), ... .
                return NotFound();
            }
            return View(data);
        }

        [HttpGet("New")]
        public IActionResult New()
        {
            // In deze methode gaan we het "nieuw" scherm laten zien. Dit is het zelfde als een edit scherm, maar dan voor een nieuw object.
            // Dit wil zeggen: géén id, géén vooraf ingevulde waarden.
            return View("Edit", new CarViewModel());
        }

        [HttpPost]
        public IActionResult Save(CarViewModel car)
        {
            // kijkt of de state van het doorgegeven model (in dit geval `person`) valid is. Valid = aan alle annotations is voldaan.
            car.Id = _modelFetcher.Save(car);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Delete(CarViewModel car)
        {
            // kijkt of de state van het doorgegeven model (in dit geval `person`) valid is. Valid = aan alle annotations is voldaan.
            car.Id = _modelFetcher.Delete(car);
            return Redirect("/");
        }
    }
}
