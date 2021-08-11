using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public abstract class AddFileBaseCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        protected AddFileBaseCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted && ViewModel.SelectedItem is TechnicalRequrements2Wrapper;
        }
    }
}