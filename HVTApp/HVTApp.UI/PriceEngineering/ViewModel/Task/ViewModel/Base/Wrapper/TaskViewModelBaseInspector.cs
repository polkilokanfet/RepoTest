using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskViewModelBaseInspector : TaskViewModel
    {
        #region ComplexProperties

        /// <summary>
        /// Проверяющий конструктор
        /// </summary>
        public UserEmptyWrapper UserConstructorInspector
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(UserConstructorInspector, value);
        }

        #endregion

        protected TaskViewModelBaseInspector(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseInspector(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        public override void InitializeComplexProperties()
        {
            base.InitializeComplexProperties();
            InitializeComplexProperty(nameof(UserConstructorInspector), Model.UserConstructorInspector == null ? null : new UserEmptyWrapper(Model.UserConstructorInspector));
        }
    }
}