using Best_Practices.Models;
using Best_Practices.Models.ModelBuilder;
using Best_Practices.Models.ModelFactory;
using Best_Practices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Best_Practices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IVehicleRepository _vehicleRepository;

        public HomeController(IVehicleRepository vehicleRepository,ILogger<HomeController> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.Vehicles = _vehicleRepository.GetVehicles();
            string error = Request.Query.ContainsKey("error") ? Request.Query["error"].ToString() : null;
            ViewBag.ErrorMessage = error;

            return View(model);
        }

        private CarFactory chooseFactory (string vehicle)
        {
            switch (vehicle) {
                case "Mustang":
                    return new FordMustangFactory();
                case "Explorer":
                    return new FordExplorerFactory();
                case "Escape":
                    return new FordEscapeFactory();
                default:
                    throw new NotImplementedException();    
            
            }
        }

        [HttpGet]
        public IActionResult AddMustang()
        {


            var carFactory = chooseFactory("Mustang");

            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult AddEscape()
        {

            var carFactory = chooseFactory("Escape");

            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }


        [HttpGet]
        public IActionResult AddExplorer()
        {


            var carFactory = chooseFactory("Explorer");

            _vehicleRepository.AddVehicle(carFactory.Create());
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult StartEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StartEngine();
                return Redirect("/");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
          
        }

        [HttpGet]
        public IActionResult AddGas(string id)
        {

            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.AddGas();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult StopEngine(string id)
        {
            try
            {
                var vehicle = _vehicleRepository.Find(id);
                vehicle.StopEngine();
                return Redirect("/");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return Redirect($"/?error={ex.Message}");
            }
           
           
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
    }
}