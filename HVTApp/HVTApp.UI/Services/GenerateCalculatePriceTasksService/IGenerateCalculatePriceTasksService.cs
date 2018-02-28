using System;
using System.Threading.Tasks;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Services
{
    public interface IGenerateCalculatePriceTasksService
    {
        Task GenerateCalculatePriceTasks(ProductWrapper productWrapper, DateTime date, Guid senderId);
    }
}

