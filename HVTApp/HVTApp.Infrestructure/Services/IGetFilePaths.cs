using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IGetFilePaths
    {
        string GetFilePath();
        IEnumerable<string> GetFilePaths();
    }
}