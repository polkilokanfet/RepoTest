using System;

namespace HVTApp.Infrastructure
{
    public interface IContainsId
    {
        Guid Id { get; }
    }
}