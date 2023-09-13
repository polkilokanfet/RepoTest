using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IGetCostsFromExcelFileService
    {
        Dictionary<string, double> GetCostsDictionary(string path);
    }
}