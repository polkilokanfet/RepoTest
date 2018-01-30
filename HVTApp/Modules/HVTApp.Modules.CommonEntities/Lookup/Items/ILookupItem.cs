using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public interface ILookupItem
    {
        string DisplayMember { get; set; }
        Guid Id { get; }
    }

    public interface ILookupItemNavigation<in TEntity> : ILookupItem
        where TEntity : class, IBaseEntity
    {
        void Refresh(TEntity entity);
    }
}