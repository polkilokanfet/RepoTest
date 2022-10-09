using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public interface IPriceEngineeringTasksViewModel : IDisposable
    {
        void Load(PriceEngineeringTasks priceEngineeringTasks);
        void Load(PriceEngineeringTask priceEngineeringTask);
    }
}