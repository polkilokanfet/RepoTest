using System;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManagerBoss : TaskViewModelBackOfficeBase
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => true;

        public TaskViewModelBackManagerBoss(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }
    }
}