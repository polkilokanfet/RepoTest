using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Wrapper.TaskScript
{
    public abstract class ScriptStepManager<TTaskViewModel> : ScriptStep<TTaskViewModel>
        where TTaskViewModel : TaskViewModel
    {
        protected ScriptStepManager(PriceEngineeringTaskStatusEnum status, TTaskViewModel viewModel) : base(status, Role.SalesManager, viewModel)
        {
        }
    }
}