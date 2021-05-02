using System;
using System.Runtime.CompilerServices;

namespace HVTApp.Infrastructure
{
    public interface IHvtAppLogger
    {
        void LogError(string message, Exception exception = null, [CallerFilePath] string fileName = "");
    }
}