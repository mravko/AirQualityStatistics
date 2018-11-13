using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public interface IDataProvider
    {
        void ImportMeasurements();

        List<YearResult> GetData(GroupingType groupingType, CalculationType calculationType);
    }
}