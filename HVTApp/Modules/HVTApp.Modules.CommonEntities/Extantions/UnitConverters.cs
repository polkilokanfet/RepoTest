using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Extantions
{
    public static class UnitConverters
    {
        public static IEnumerable<OfferUnitGroup> ConvertToGroup(this IEnumerable<OfferUnit> offerUnits)
        {
            var groups = offerUnits.GroupBy(x => x, new OfferUnitComparer());
            foreach (var group in groups)
                yield return new OfferUnitGroup(group.ToList());
        }

        public static IEnumerable<ProjectUnitsGrouped> ConvertToGroup(this IEnumerable<ProjectUnitWrapper> projectUnitWrappers)
        {
            var groups = projectUnitWrappers.Select(x => x.Model).GroupBy(x => x, new ProjectUnitComparer());
            foreach (var group in groups)
            {
                var wrappers = projectUnitWrappers.Where(x => group.Contains(x.Model));
                yield return new ProjectUnitsGrouped(wrappers);
                
            }
        }

    }
}