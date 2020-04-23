using System.Collections.Generic;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
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