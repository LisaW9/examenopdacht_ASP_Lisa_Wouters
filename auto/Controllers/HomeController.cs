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
        private ICarModelFetcher _carModelFetcher;
        private IOwnerModelFetcher _ownerModelFetcher;
        private ITypeModelFetcher _typeModelFetcher;

        public HomeController(ICarModelFetcher carModelFetcher, IOwnerModelFetcher ownerModelFetcher, ITypeModelFetcher typeModelFetcher)
        {
            _carModelFetcher = carModelFetcher;
            _ownerModelFetcher = ownerModelFetcher;
            _typeModelFetcher = typeModelFetcher;
        }
        public IActionResult Index()
        {
            var model = new CarIndexViewModel
            {
                Cars = _carModelFetcher.GetAllCarViewModels()
            };

            return View(model);
        }

        public IActionResult Owners()
        {
            var model = new OwnerIndexViewModel
            {
                Owners = _ownerModelFetcher.GetAllOwnerViewModels()
            };

            return View(model);
        }

        public IActionResult Types()
        {
            var model = new TypeIndexViewModel
            {
                Types = _typeModelFetcher.GetAllTypeViewModels()
            };

            return View(model);
        }
    }
}
