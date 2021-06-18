using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;
using NorbitsChallenge.Bll;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            var companyId = model.CompanyId;
            var carDb = new CarDb(_config);
            var allCars = carDb.GetAllCarObjects(companyId);

            model.AllCars = allCars;


            return View(model);
        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            var carDb = new CarDb(_config);

            var tireCount = carDb.GetTireCount(companyId, licensePlate);
            var searchedCar = carDb.GetCarByLicensePlate(companyId, licensePlate);
            var allCars = carDb.GetAllCarObjects(companyId);

            var model = GetCompanyModel();

            model.searchedCar = searchedCar;

            //sets model values from db
            
            model.TireCount = tireCount;
            model.AllCars = allCars;

            return Json(model.searchedCar);
        }

        public IActionResult UpdateCar(string OldLicensePlate, string CarModel, string CarBrand, string CarDescription, int TireCount)
        {
            CarUpdateModel car = new ();
            car.CarLicensePlate = OldLicensePlate;
            car.CarModel = CarModel;
            car.CarBrand = CarBrand;
            car.CarDescription = CarDescription;
            car.TireCount = TireCount;

            return View(car);
        }

        [HttpPost]
        public IActionResult UpdateCar(CarUpdateModel car)
        {
            var carDb = new CarDb(_config);
            var companyModel = GetCompanyModel();
            var existingCar = carDb.GetCarByLicensePlate(companyModel.CompanyId, car.CarLicensePlate);
            if (!String.IsNullOrEmpty(existingCar.LicensePlate))
            {
                ViewData["ErrorMessage"] = "Car already exists in database";
                return View();
            }
            carDb.UpdateCar(car.CarLicensePlate, car.OldLicensePlate, car.CarModel, car.CarBrand, car.CarDescription, car.TireCount, companyModel.CompanyId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCar(string licensePlate, int companyId)
        {
            var carDb = new CarDb(_config);
            carDb.DeleteCar(licensePlate, companyId);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult AddCar()
        {
            ViewData["Message"] = "Add car";

            return View();
        }

        [HttpPost]
        public IActionResult AddCar(CarInputModel model)
        {
            var carDb = new CarDb(_config);
            var companyModel = GetCompanyModel();
            var existingCar = carDb.GetCarByLicensePlate(companyModel.CompanyId, model.CarLicensePlate);
            if (!String.IsNullOrEmpty(existingCar.LicensePlate))
            {
                ViewData["ErrorMessage"] = "Car already exists in database";
                return View();
            }

            if (ModelState.IsValid)
            {
                carDb.CreateCar(companyModel.CompanyId,
                    model.CarLicensePlate,
                    model.CarModel,
                    model.CarBrand,
                    model.TireCount,
                    model.CarDescription);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName };
        }
    }
}
