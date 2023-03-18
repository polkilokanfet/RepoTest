using System;
using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBaseWithStartCommand : TaskViewModel
    {
        public DoStepCommandStart StartCommand { get; private set; }

        protected TaskViewModelBaseWithStartCommand(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseWithStartCommand(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        protected override void InCtor()
        {
            base.InCtor();
            StartCommand = new DoStepCommandStart(this, this.Container);
        }
    }
}