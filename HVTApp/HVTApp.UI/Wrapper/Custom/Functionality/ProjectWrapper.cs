using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapper : IWrapperWithUnits<SalesUnitWrapper>
    {
        public ValidatableChangeTrackingCollection<SalesUnitWrapper> Units { get; } = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(new List<SalesUnitWrapper>());

        public ProjectWrapper(Project project, IEnumerable<SalesUnitWrapper> units) : this(project)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(units);
            RegisterCollectionWithoutSynch(Units);
        }
    }
}
