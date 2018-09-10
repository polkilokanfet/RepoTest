using System.Collections.Generic;

namespace HVTApp.UI.Wrapper
{
    public partial class OrderWrapper
    {
        public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; } = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(new List<SalesUnitWrapper>());

        public override void InitializeOther()
        {
            RegisterCollectionWithoutSynch(SalesUnits);
        }
    }
}