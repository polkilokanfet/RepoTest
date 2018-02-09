using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Extantions
{
    public static class UnitConverters
    {
        public static IEnumerable<ProjectUnitGroup> ConvertToGroup(this IEnumerable<ProjectUnit> projectUnits)
        {
            var groups = projectUnits.GroupBy(x => x, new ProjectUnitComparer());
            foreach (var group in groups)
                yield return new ProjectUnitGroup(group.ToList());
        }

        public static IEnumerable<OfferUnitGroup> ConvertToGroup(this IEnumerable<OfferUnit> offerUnits)
        {
            var groups = offerUnits.GroupBy(x => x, new OfferUnitComparer());
            foreach (var group in groups)
                yield return new OfferUnitGroup(group.ToList());
        }
    }
}