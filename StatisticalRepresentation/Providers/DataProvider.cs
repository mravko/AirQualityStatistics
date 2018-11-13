using System;
using System.Collections.Generic;
using TinyCsvParser;

namespace StatisticalRepresentation
{
    public class DataProvider : IDataProvider
    {
        private List<YearMeasurements> Measurements = new List<YearMeasurements>();

        public DataProvider()
        {
            var mapping = new MeasurementMapping();
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ';');
            CsvParser<Measurement> parser = new CsvParser<Measurement>(csvParserOptions, mapping);

            var years = new List<int>
            {
                2015, 2016, 2017
            };
            years.ForEach(year =>
            {
                try
                {
                    Measurements.Add(parser.ImportMeasurementsForYear(year));
                }
                catch (Exception e)
                {
                    //todo: implement logging
                }
            });
            
        }

        public List<YearResult> GetData(GroupingType groupingType, CalculationType calculationType)
        {
            return Measurements.GetResults(groupingType, calculationType);
        }
    }
}
