using System;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelWithLoadHistoryCommand : TaskViewModel
    {
        public ICommandRaiseCanExecuteChanged LoadFilesCommand { get; }

        protected TaskViewModelWithLoadHistoryCommand(IUnityContainer container, Guid priceEngineeringTaskId)
            : base(container, priceEngineeringTaskId)
        {
            LoadFilesCommand = new LoadHistoryTaskCommand(
                this.Model, 
                container.Resolve<IFilesStorageService>(), 
                container.Resolve<IPrintPriceEngineering>(), 
                container.Resolve<IMessageService>());
        }
    }
}