using System;

namespace HVTApp.Model.Services
{
    public interface IFileStorage
    {
        Guid Id { get; }
        string Name { get; }
    }
}