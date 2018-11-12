using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public class MonthResult
    {
        public int MonthIndex { get; set; }

        public string MonthName
        {
            get
            {
                switch (MonthIndex)
                {
                    case 1:
                        return "January";
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                    default:
                        return "UNKNOWN MONTH";
                }
            }
        }

        public Dictionary<string, double> Result { get; set; } = new Dictionary<string, double>();
    }
}
