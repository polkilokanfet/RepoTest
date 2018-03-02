using System.Collections.Generic;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapper
    {
        public IEnumerable<UnitGroup> UnitGroups => SalesUnits.ToUnitGroups();
    }
}
