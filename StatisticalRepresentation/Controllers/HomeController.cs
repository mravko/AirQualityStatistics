using Microsoft.AspNetCore.Mvc;
using StatisticalRepresentation.Models;
using System.Diagnostics;

namespace StatisticalRepresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider dataProvider;

        public HomeController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var data = dataProvider.GetData_AverageByMonth();
            return View(data);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
