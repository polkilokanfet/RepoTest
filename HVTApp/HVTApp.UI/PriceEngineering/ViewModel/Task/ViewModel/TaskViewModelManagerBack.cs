using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelManagerBack : TaskViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => true;

        public TaskViewModelManagerBack(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }
    }
}