using System.Collections.Generic;

namespace StatisticalRepresentation
{
    public interface IDataProvider
    {
        List<YearResult> GetData(GroupingType groupingType, CalculationType calculationType);
    }
}