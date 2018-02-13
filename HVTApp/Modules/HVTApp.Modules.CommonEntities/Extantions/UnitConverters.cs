using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Extantions
{
    public static class UnitConverters
    {
        public static IEnumerable<ProjectUnitsGrouped> ConvertToGroup(this IEnumerable<ProjectUnitWrapper> projectUnitWrappers)
        {
            var wrappers = projectUnitWrappers.ToList();
            var groups = wrappers.Select(x => x.Model).GroupBy(x => x, new ProjectUnitComparer());
            foreach (var group in groups)
            {
                yield return new ProjectUnitsGrouped(wrappers.Where(x => group.Contains(x.Model)));
            }
        }

        public static IEnumerable<OfferUnitsGrouped> ConvertToGroup(this IEnumerable<OfferUnitWrapper> offerUnitWrappers)
        {
            var wrappers = offerUnitWrappers.ToList();
            var groups = wrappers.Select(x => x.Model).GroupBy(x => x, new OfferUnitComparer());
            foreach (var group in groups)
            {
                yield return new OfferUnitsGrouped(wrappers.Where(x => group.Contains(x.Model)));
            }
        }

    }
}