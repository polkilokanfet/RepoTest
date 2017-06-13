namespace HVTApp.Infrastructure
{
    public interface IWrapper<TModel>
        where TModel : class, IBaseEntity
    {
        TModel Model { get; }
        bool EqualsModels(IWrapper<TModel> wrapper);
    }
}