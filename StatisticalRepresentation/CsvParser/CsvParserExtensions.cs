﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace StatisticalRepresentation
{
    public static class CsvParserExtensions
    {
        public static YearMeasurements ImportMeasurementsForYear(this CsvParser<Measurement> parser, int year)
        {
            var result = parser
               .ReadFromFile(string.Format(@"ExportedMeasurements{1}measurements_{0}.csv", year, Path.DirectorySeparatorChar), Encoding.UTF8)
               .ToList();

            return new YearMeasurements
            {
                Year = year,
                Measurements = result.Where(x => x.IsValid && x.Result != null && x.Result.Data != null)
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
}
