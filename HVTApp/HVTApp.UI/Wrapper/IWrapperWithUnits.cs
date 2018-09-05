namespace HVTApp.UI.Wrapper
{
    public interface IWrapperWithUnits<TUnit>
        where TUnit : class, IUnit
    {
        ValidatableChangeTrackingCollection<TUnit> Units { get; }
    }
}