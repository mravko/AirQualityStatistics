using System.Collections.Generic;
using TinyCsvParser;

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

        public List<YearResult> GetData(GroupingType groupingType, CalculationType calculationType)
        {
            return Measurements.GetResults(groupingType, calculationType);
        }
    }
}
