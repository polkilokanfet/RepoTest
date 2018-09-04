namespace HVTApp.UI.Wrapper
{
    public interface IWrapperWithUnits<TUnit>
        where TUnit : class, IProductUnit
    {
        ValidatableChangeTrackingCollection<TUnit> Units { get; }
    }
}