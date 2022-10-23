using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintPriceEngineering
    {
        void PrintPriceEngineeringTasks(Guid id);
        string PrintPriceEngineeringTask(Guid id, string destDirectory = null, string fileName = null);
    }
}