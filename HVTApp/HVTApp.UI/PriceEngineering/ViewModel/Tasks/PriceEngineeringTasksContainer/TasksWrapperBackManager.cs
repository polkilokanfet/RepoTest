using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperBackManager : TasksWrapper<TaskViewModelBackManager>
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

        #region CommentBackOfficeBoss

        /// <summary>
        /// Комментарий руководителя бэкофиса
        /// </summary>
        public string CommentBackOfficeBoss => Model.CommentBackOfficeBoss;

        #endregion

        #region BackManager

        public UserEmptyWrapper BackManager => new UserEmptyWrapper(Model.BackManager);

        #endregion

        public TasksWrapperBackManager(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelBackManager GetChildPriceEngineeringTask(IUnityContainer container, Guid id)
        {
            return new TaskViewModelBackManager(this, container, id);
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(TceNumber))
                yield return new ValidationResult("TceNumber is required", new[] {nameof(TceNumber)});
        }
    }
}