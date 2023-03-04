using System;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManager : TaskViewModelBackOfficeBase
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => true;

        public TaskViewModelBackManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }
    }
}