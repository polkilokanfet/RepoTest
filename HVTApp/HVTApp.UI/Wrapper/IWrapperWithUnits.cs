namespace HVTApp.UI.Wrapper
{
    public interface IWrapperWithUnits<TUnit>
        where TUnit : class, IUnitWrapper
    {
        ValidatableChangeTrackingCollection<TUnit> Units { get; }
    }
}