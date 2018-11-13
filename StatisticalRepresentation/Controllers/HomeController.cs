using Microsoft.AspNetCore.Mvc;
using StatisticalRepresentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StatisticalRepresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider dataProvider;

        public HomeController(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        public class Test
        {
            public DateTime Date { get; set; }
            public int Value { get; set; }
        }
        public IActionResult Index()
        {
            var dates = new List<Test>();
            dates.Add(new Test { Date = new DateTime(2015, 1, 1), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 1, 1), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 1, 2), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 2, 1), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 2, 1), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 3, 1), Value = 1 });
            dates.Add(new Test { Date = new DateTime(2015, 3, 1), Value = 1 });

            var s = dates.GroupBy(x => x.Date.Month);

            var s1 = s.First().GroupBy(x => x.Date.Day);

            return View();
        }

        public IActionResult MonthAverage()
        {
            var data = dataProvider.GetData(GroupingType.ByMonth, CalculationType.Average);
            return View(data);
        }

        public IActionResult MonthMax()
        {
            var data = dataProvider.GetData(GroupingType.ByMonth, CalculationType.Max);
            return View(data);
        }

        public IActionResult MonthMedian()
        {
            var data = dataProvider.GetData(GroupingType.ByMonth, CalculationType.Median);
            return View(data);
        }

        public IActionResult MonthStandardDeviation()
        {
            var data = dataProvider.GetData(GroupingType.ByMonth, CalculationType.StandardDeviation);
            return View(data);
        }

        public IActionResult DayAverage()
        {
            var data = dataProvider.GetData(GroupingType.ByDay, CalculationType.Average);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
