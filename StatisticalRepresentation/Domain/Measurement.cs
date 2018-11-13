using System;
using System.Globalization;

namespace StatisticalRepresentation
{
    public class Measurement
    {
        public string StationName { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public double? Data { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? WindSpeed { get; set; }
        public decimal? Percipitation { get; set; }
    }
}
