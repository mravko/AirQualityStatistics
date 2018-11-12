using TinyCsvParser.Mapping;

namespace StatisticalRepresentation
{
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
}
