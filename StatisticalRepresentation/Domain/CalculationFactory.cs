using MathNet.Numerics.Statistics;
using System;
using System.Linq;

namespace StatisticalRepresentation
{
    public static class CalculationFactory
    {
        public static Action<ResultGroup, IGrouping<string, Measurement>> GetCalculationFunction(CalculationType type)
        {
            switch (type)
            {
                case CalculationType.Average:
                    return (resultData, sd) =>
                    {
                        var stationAverageData = sd.Select(x => x.Data.Value).Average();
                        resultData.Result.Add(sd.Key, stationAverageData);
                    };
                case CalculationType.Median:
                    return (resultData, sd) =>
                    {
                        var Mediation = sd.Select(x => x.Data).Median();
                        resultData.Result.Add(sd.Key, Mediation);
                    };
                case CalculationType.Max:
                    return (resultData, sd) =>
                    {
                        var maxData = sd.Max(x => x.Data);
                        resultData.Result.Add(sd.Key, maxData.Value);
                    };
                case CalculationType.StandardDeviation:
                    return (resultData, sd) =>
                    {
                        var stationStandardDeviationData = sd.Select(x => x.Data).StandardDeviation();
                        resultData.Result.Add(sd.Key, stationStandardDeviationData);
                    };
                default:
                    throw new ApplicationException("Cannot get calc function for calc type");
            }
        }
    }
}
