using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapper : IWrapperWithUnits<SalesUnitWrapper>
    {
        public ValidatableChangeTrackingCollection<SalesUnitWrapper> Units { get; }

        public SpecificationWrapper(Specification specification, IEnumerable<SalesUnitWrapper> units) : this(specification)
        {
            Units = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(units);
            RegisterCollectionWithoutSynch(Units);
        }
    }
}
