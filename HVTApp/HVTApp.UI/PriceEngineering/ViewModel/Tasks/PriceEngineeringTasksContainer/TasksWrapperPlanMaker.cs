using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperPlanMaker : TasksWrapper<TaskViewModelPlanMaker>
    {
        #region TceNumber

        /// <summary>
        /// Номер ТСЕ
        /// </summary>
        public string TceNumber
        {
            get => Model.TceNumber;
            set => SetValue(value);
        }
        public string TceNumberOriginalValue => GetOriginalValue<string>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        #endregion

        public TasksWrapperPlanMaker(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelPlanMaker GetChildPriceEngineeringTask(IUnityContainer container, Guid childTaskId)
        {
            return new TaskViewModelPlanMaker(this, container, childTaskId);
        }

        protected override IEnumerable<TaskViewModelPlanMaker> GetChildPriceEngineeringTasks(IUnityContainer container)
        {
            return this.Model.ChildPriceEngineeringTasks
                .Where(x => x.Status.Equals(ScriptStep.ProductionRequestStart) || x.Status.Equals(ScriptStep.ProductionRequestFinish))
                .Select(task => this.GetChildPriceEngineeringTask(container, task.Id));
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(TceNumber))
                yield return new ValidationResult("TceNumber is required", new[] { nameof(TceNumber) });
        }
    }
}