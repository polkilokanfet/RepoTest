using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public abstract class BaseTechnicalRequrementsTaskViewModelCommand : DelegateLogCommand
    {
        protected TechnicalRequrementsTaskViewModel ViewModel { get; }
        protected IUnityContainer Container { get; }
        protected IMessageService MessageService { get; }
        protected IUnitOfWork UnitOfWork => ViewModel.UnitOfWork1;

        protected BaseTechnicalRequrementsTaskViewModelCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container)
        {
            ViewModel = viewModel;
            Container = container;
            MessageService = Container.Resolve<IMessageService>();
        }

        protected abstract override void ExecuteMethod();
    }
}