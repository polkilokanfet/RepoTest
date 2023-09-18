using System.Collections.Generic;

namespace HVTApp.Infrastructure.Services
{
    public interface IGetCostsFromExcelFileService
    {
        Dictionary<string, double> GetCostsDictionaryFromR3File(string path);
        Dictionary<string, double> GetCostsDictionaryFromCalculationFile(string path);
    }
}