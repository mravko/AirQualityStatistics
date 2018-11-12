using System.Collections.Generic;
using System.Linq;
using TinyCsvParser;
using MathNet.Numerics.Statistics;

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
            yearResult.Type = "PM10";

            var groupedByMonth = Measurements.Where(x => x.Year == year).SelectMany(x => x.Measurements).Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").GroupBy(x => x.DateTime.Month).ToList();

            foreach (var monthData in groupedByMonth)
            {
                var monthResult = new MonthResult();
                monthResult.MonthIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName).OrderBy(x => x.Key);
                foreach (var sd in stationData)
                {
                    var stationAverageData = sd.Select(x => x.Data ?? 0).Average();
                    monthResult.Result.Add(sd.Key, stationAverageData);
                }

                yearResult.MonthResult.Add(monthResult);
            }

            yearResult.MonthResult = yearResult.MonthResult.OrderBy(x => x.MonthIndex).ToList();
            return yearResult;
        }

        public YearResult GetData_MaxByMonthForYear(int year)
        {
            var yearResult = new YearResult();

            yearResult.Year = year;
            yearResult.Type = "PM10";

            var groupedByMonth = Measurements.Where(x => x.Year == year).SelectMany(x => x.Measurements).Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").GroupBy(x => x.DateTime.Month).ToList();

            foreach (var monthData in groupedByMonth)
            {
                var monthResult = new MonthResult();
                monthResult.MonthIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName).OrderBy(x => x.Key);
                foreach (var sd in stationData)
                {
                    var maxData = sd.Max(x => x.Data ?? 0);
                    monthResult.Result.Add(sd.Key, maxData);
                }

                yearResult.MonthResult.Add(monthResult);
            }

            yearResult.MonthResult = yearResult.MonthResult.OrderBy(x => x.MonthIndex).ToList();
            return yearResult;
        }

        public YearResult GetData_MedianByMonthForYear(int year)
        {
            var yearResult = new YearResult();

            yearResult.Year = year;
            yearResult.Type = "PM10";

            var groupedByMonth = Measurements.Where(x => x.Year == year).SelectMany(x => x.Measurements).Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").GroupBy(x => x.DateTime.Month).ToList();

            foreach (var monthData in groupedByMonth)
            {
                var monthResult = new MonthResult();
                monthResult.MonthIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName).OrderBy(x => x.Key);
                foreach (var sd in stationData)
                {
                    var stationAverageData = sd.Select(x=> x.Data ?? 0).Median();
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

        public List<YearResult> GetData_MaxByMonth()
        {
            var toReturn = new List<YearResult>();
            toReturn.Add(GetData_MaxByMonthForYear(2015));
            toReturn.Add(GetData_MaxByMonthForYear(2016));
            toReturn.Add(GetData_MaxByMonthForYear(2017));

            return toReturn;
        }

        public List<YearResult> GetData_MedianByMonth()
        {
            var toReturn = new List<YearResult>();
            toReturn.Add(GetData_MedianByMonthForYear(2015));
            toReturn.Add(GetData_MedianByMonthForYear(2016));
            toReturn.Add(GetData_MedianByMonthForYear(2017));

            return toReturn;
        }
    }
}
