using System;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Tce.Second;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelBackManager : TaskViewModelBackOfficeBase
    {
        public override bool IsTarget => true;

        public override bool IsEditMode => this.Model.Status.Equals(ScriptStep.LoadToTceStart);

        public TasksTceItem TasksTceItem { get; }

        public TaskViewModelBackManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
            TasksTceItem = new TasksTceItem(this.Model);

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(IsEditMode));
            };
        }
    }
}