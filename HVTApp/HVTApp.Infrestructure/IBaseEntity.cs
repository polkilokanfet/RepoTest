using System;

namespace HVTApp.Infrastructure
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
    }
}