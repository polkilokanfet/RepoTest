using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        protected override TaskViewModelPlanMaker GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelPlanMaker(this, container, id);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(TceNumber))
                yield return new ValidationResult("TceNumber is required", new[] { nameof(TceNumber) });
        }
    }
}