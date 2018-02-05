using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Extantions
{
    public static class UnitConverters
    {
        public static IEnumerable<ProjectUnitGroup> ConvertToGroup(this IEnumerable<ProjectUnit> projectUnits)
        {
            var groups = projectUnits.GroupBy(x => x, new ProjectUnitComparer());
            foreach (var group in groups)
                yield return new ProjectUnitGroup { ProjectUnits = group.ToList() };
        }
    }
}