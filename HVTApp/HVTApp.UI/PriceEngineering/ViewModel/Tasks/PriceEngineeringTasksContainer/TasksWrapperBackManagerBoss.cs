using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer
{
    public class TasksWrapperBackManagerBoss : TasksWrapper<TaskViewModelBackManagerBoss>
    {
        /// <summary>
        /// Номер ТСЕ
        /// </summary>
        public string TceNumber => Model.TceNumber;

        #region CommentBackOfficeBoss

        /// <summary>
        /// Комментарий руководителя бэкофиса
        /// </summary>
        public string CommentBackOfficeBoss
        {
            get => Model.CommentBackOfficeBoss;
            set => SetValue(value);
        }
        public string CommentBackOfficeBossOriginalValue => GetOriginalValue<string>(nameof(CommentBackOfficeBoss));
        public bool CommentBackOfficeBossIsChanged => GetIsChanged(nameof(CommentBackOfficeBoss));

        #endregion

        #region BackManager

        public UserEmptyWrapper BackManager
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(BackManager, value);
        }

        #endregion

        public TasksWrapperBackManagerBoss(PriceEngineeringTasks model, IUnityContainer container) : base(model, container)
        {
        }

        protected override TaskViewModelBackManagerBoss GetChildPriceEngineeringTask(IUnityContainer container, Guid childTaskId)
        {
            return new TaskViewModelBackManagerBoss(container, childTaskId);
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserEmptyWrapper(Model.BackManager));
        }
    }
}