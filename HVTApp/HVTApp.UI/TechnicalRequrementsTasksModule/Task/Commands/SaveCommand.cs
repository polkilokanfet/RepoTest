using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class SaveCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public SaveCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (UnitOfWork.SaveEntity(ViewModel.TechnicalRequrementsTaskWrapper.Model).OperationCompletedSuccessfully)
            {
                ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();
                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);
            }

            this.RaiseCanExecuteChanged();
        }

        protected override bool CanExecuteMethod()
        {
            if (ViewModel.IsValid == false) return false;
            if (ViewModel.IsChanged == false) return false;

            if (ViewModel.CurrentUserIsManager)
                if (ViewModel.WasStarted) return false;
            else if (ViewModel.CurrentUserIsBackManager)
                if (ViewModel.AllowFinish) return false;

            return true;
        }
    }
}