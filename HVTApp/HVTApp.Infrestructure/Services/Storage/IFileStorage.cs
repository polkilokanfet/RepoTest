﻿using System;

namespace HVTApp.Infrastructure.Services.Storage
{
    public interface IFileStorage
    {
        Guid Id { get; }
        string Name { get; }
        DateTime CreationMoment { get; }
        bool IsActual { get; }
    }
}