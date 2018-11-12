using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public class YearMeasurements
    {
        public int Year { get; set; }
        public List<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
