using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapper : IWrapperWithUnits<SalesUnitWrapper>
    {
        public IEnumerable<UnitGroup> UnitGroups { get; set; }
        public ValidatableChangeTrackingCollection<SalesUnitWrapper> Units { get; }

        public ProjectWrapper(Project project, IEnumerable<SalesUnitWrapper> units) : this(project)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(units);
        }
    }
}
