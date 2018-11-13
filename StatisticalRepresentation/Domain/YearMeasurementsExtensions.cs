using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public static class YearMeasurementsExtensions
    {
        public static List<YearResult> GetResults(this List<YearMeasurements> yearMeasurements, GroupingType groupingType, CalculationType calculationType)
        {
            var toReturn = new List<YearResult>();

            foreach (var yearMeasurement in yearMeasurements)
            {
                toReturn.Add(yearMeasurement.GetResultFor(groupingType, calculationType));
            }

            return toReturn;
        }
    }
}
