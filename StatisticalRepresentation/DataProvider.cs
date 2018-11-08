using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace StatisticalRepresentation
{
    public class DataProvider : IDataProvider
    {
        private List<YearMeasurements> Measurements = new List<YearMeasurements>();

        public void ImportMeasurements()
        {
            var mapping = new MeasurementMapping();
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            CsvParser<Measurement> parser = new CsvParser<Measurement>(csvParserOptions, mapping);

            Measurements.Add(parser.ImportMeasurementsForYear(2015));
            Measurements.Add(parser.ImportMeasurementsForYear(2016));
            Measurements.Add(parser.ImportMeasurementsForYear(2017));
        }

        public YearResult GetData_AverageByMonthForYear(int year)
        {
            var yearResult = new YearResult();

            yearResult.Year = year;
            yearResult.Type = "PM25";

            var groupedByMonth = Measurements.Where(x => x.Year == year).SelectMany(x => x.Measurements).Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").GroupBy(x => x.DateTime.Month).ToList();

            foreach (var monthData in groupedByMonth)
            {
                var monthResult = new MonthResult();
                monthResult.MonthIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName);
                foreach (var sd in stationData)
                {
                    var stationAverageData = sd.Sum(x => x.Data ?? 0) / sd.Count();
                    monthResult.Result.Add(sd.Key, stationAverageData);
                }

                yearResult.MonthResult.Add(monthResult);
            }

            yearResult.MonthResult = yearResult.MonthResult.OrderBy(x => x.MonthIndex).ToList();
            return yearResult;
        }

        public List<YearResult> GetData_AverageByMonth()
        {
            var toReturn = new List<YearResult>();
            toReturn.Add(GetData_AverageByMonthForYear(2015));
            toReturn.Add(GetData_AverageByMonthForYear(2016));
            toReturn.Add(GetData_AverageByMonthForYear(2017));

            return toReturn;
        }
    }

    public static class CsvParserExtensions
    {
        public static YearMeasurements ImportMeasurementsForYear(this CsvParser<Measurement> parser, int year)
        {
            var result = parser
               .ReadFromFile(string.Format(@"measurements_{0}.csv", year), Encoding.UTF8)
               .ToList();

            return new YearMeasurements
            {
                Year = year,
                Measurements = result.Where(x => x.IsValid && x.Result != null)
                .Select(x => x.Result)

                .Where(x => x.StationName == "Centar"
                || x.StationName == "GaziBaba"
                || x.StationName == "Karpos"
                || x.StationName == "Lisice"
                || x.StationName == "Rektorat"
                || x.StationName == "Miladinovci"
                || x.StationName == "Tetovo"
                ).ToList()
            };
        }
    }

    public class YearResult
    {
        public string Type { get; set; }
        public int Year { get; set; }
        public List<MonthResult> MonthResult = new List<MonthResult>();
    }

    public class MonthResult
    {
        public int MonthIndex { get; set; }

        public string MonthName
        {
            get
            {
                switch (MonthIndex)
                {
                    case 1:
                        return "January";
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                    default:
                        return "UNKNOWN MONTH";
                }
            }
        }

        public Dictionary<string, decimal> Result { get; set; } = new Dictionary<string, decimal>();
    }

    public class MeasurementMapping : CsvMapping<Measurement>
    {
        public MeasurementMapping()
        {
            MapProperty(0, x => x.StationName);
            MapProperty(1, x => x.DateTime);
            MapProperty(2, x => x.Data);
            MapProperty(3, x => x.Type);
            MapProperty(4, x => x.Temperature);
            MapProperty(5, x => x.Humidity);
            MapProperty(6, x => x.WindSpeed);
            MapProperty(7, x => x.Percipitation);
        }
    }

    public class Measurement
    {
        public string StationName { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public decimal? Data { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? WindSpeed { get; set; }
        public decimal? Percipitation { get; set; }
    }

    public class YearMeasurements
    {
        public int Year { get; set; }
        public List<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
