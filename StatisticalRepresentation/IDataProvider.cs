using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public interface IDataProvider
    {
        void ImportMeasurements();
        YearResult GetData_AverageByMonthForYear(int year);

        List<YearResult> GetData_AverageByMonth();
        List<YearResult> GetData_MaxByMonth();
        List<YearResult> GetData_MedianByMonth();
    }
}