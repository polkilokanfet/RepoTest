using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public interface IWrapperWithUnits<TUnit>
        where TUnit : class, IUnitWrapper
    {
        ValidatableChangeTrackingCollection<TUnit> Units { get; }
    }
}