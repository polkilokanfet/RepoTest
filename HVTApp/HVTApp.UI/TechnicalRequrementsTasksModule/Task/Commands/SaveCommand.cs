using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
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
            ViewModel.TechnicalRequrementsTaskWrapper.AcceptChanges();

            var technicalRequrementsTask = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(ViewModel.TechnicalRequrementsTaskWrapper.Model.Id);
            if (technicalRequrementsTask == null)
            {
                UnitOfWork.Repository<TechnicalRequrementsTask>().Add(ViewModel.TechnicalRequrementsTaskWrapper.Model);
            }

            UnitOfWork.SaveChanges();

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            this.RaiseCanExecuteChanged();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsChanged &&
                   !ViewModel.WasStarted;
        }
    }
}