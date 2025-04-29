using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public interface ITasksViewModel<out T> where T : ITasksWrapper
    {
        T TasksWrapper { get; }
    }
}