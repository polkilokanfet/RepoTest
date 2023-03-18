using System;
using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBaseStartable : TaskViewModel
    {
        public DoStepCommandStart StartCommand { get; private set; }

        protected TaskViewModelBaseStartable(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseStartable(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        protected override void InCtor()
        {
            base.InCtor();
            StartCommand = new DoStepCommandStart(this, this.Container);
        }
    }
}