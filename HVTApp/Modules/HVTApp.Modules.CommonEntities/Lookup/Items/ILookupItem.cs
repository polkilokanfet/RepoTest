using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Lookup
{
    public interface ILookupItem
    {
        string DisplayMember { get; set; }
        Guid Id { get; set; }

        void Refresh(IBaseEntity entity);
    }
}