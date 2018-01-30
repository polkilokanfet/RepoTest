namespace HVTApp.Infrastructure
{
    public interface ILookupItemNavigation<TEntity> : ILookupItem
        where TEntity : class, IBaseEntity
    {
        TEntity Entity { get; }
        void Refresh(TEntity entity);
    }
}