using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManagerBoss : TaskViewModel
    {
        public override bool IsTarget => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public TaskViewModelBackManagerBoss(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }
    }
}