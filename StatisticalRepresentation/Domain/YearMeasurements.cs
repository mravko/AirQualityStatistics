using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticalRepresentation
{
    public class YearMeasurements
    {
        public int Year { get; set; }
        public List<Measurement> Measurements { get; set; } = new List<Measurement>();

        public YearResult GetResultFor(GroupingType groupingType, CalculationType calculationType)
        {

            switch (groupingType)
            {
                case GroupingType.ByDay:
                    return GetDataByDay(CalculationFactory.GetCalculationFunction(calculationType));
                case GroupingType.ByMonth:
                    return GetDataByMonth(CalculationFactory.GetCalculationFunction(calculationType));
                default:
                    throw new ApplicationException("Invalid grouping type when getting results");
            }
        }

        private YearResult GetDataByDay(Action<ResultGroup, IGrouping<string, Measurement>> action)
        {
            var yearResult = new YearResult();

            yearResult.Year = Year;
            yearResult.Type = "PM10";

            var s = Measurements.Select(x => x.DateTime.Day).Max();
            var g1 = Measurements.Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").ToList();
            var g2 = g1.GroupBy(x => x.DateTime.Month).ToList();
            var g3 = g2.First().ToList();
            var g4 = g3.GroupBy(x => x.DateTime.Day).ToList();
            var groupedByMonth = g4.ToList();

            foreach (var monthData in groupedByMonth)
            {
                var resultData = new ResultGroup();
                resultData.ResultIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName).OrderBy(x => x.Key);
                foreach (var sd in stationData)
                {
                    action(resultData, sd);
                }

                yearResult.MonthResult.Add(resultData);
            }

            yearResult.MonthResult = yearResult.MonthResult.OrderBy(x => x.ResultIndex).ToList();
            return yearResult;
        }

        private YearResult GetDataByMonth(Action<ResultGroup, IGrouping<string, Measurement>> action)
        {
            var yearResult = new YearResult();

            yearResult.Year = Year;
            yearResult.Type = "PM10";

            var groupedByMonth = Measurements.Where(x => x.DateTime != null && x.Type != null && x.Type == "PM10").GroupBy(x => x.DateTime.Month).ToList();

            foreach (var monthData in groupedByMonth)
            {
                var resultData = new ResultGroup();
                resultData.ResultIndex = monthData.Key;

                var stationData = monthData.GroupBy(x => x.StationName).OrderBy(x => x.Key);
                foreach (var sd in stationData)
                {
                    action(resultData, sd);
                }

                yearResult.MonthResult.Add(resultData);
            }

            yearResult.MonthResult = yearResult.MonthResult.OrderBy(x => x.ResultIndex).ToList();
            return yearResult;
        }
    }
}
