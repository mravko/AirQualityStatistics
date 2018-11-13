using System;

namespace StatisticalRepresentation
{
    public static class GroupingFunctionFactory
    {
        public static Func<Measurement, int> GetGroupingSelector(GroupingType groupingType)
        {
            switch (groupingType)
            {
                case GroupingType.ByDay:
                    return x => x.DateTime.Day;
                case GroupingType.ByMonth:
                    return x => x.DateTime.Month;
                default:
                    throw new ApplicationException("Cannot get grouping selector");
            }
        }
    }
}
