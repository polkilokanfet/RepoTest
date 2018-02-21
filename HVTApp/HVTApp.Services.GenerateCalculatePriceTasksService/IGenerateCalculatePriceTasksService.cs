using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Services.GenerateCalculatePriceTasksService
{
    public interface IGenerateCalculatePriceTasksService
    {
        Task<IEnumerable<CalculatePriceTask>> GenerateCalculatePriceTasks(ProductWrapper productWrapper, DateTime date);
    }
}

