using System;

namespace HVTApp.Infrastructure
{
    public interface ILookupItem
    {
        string DisplayMember { get; set; }
        Guid Id { get; }
    }
}