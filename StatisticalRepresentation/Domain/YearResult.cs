using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public class YearResult
    {
        public string Type { get; set; }
        public int Year { get; set; }

        public List<ResultGroup> MonthResult = new List<ResultGroup>();
    }
}
