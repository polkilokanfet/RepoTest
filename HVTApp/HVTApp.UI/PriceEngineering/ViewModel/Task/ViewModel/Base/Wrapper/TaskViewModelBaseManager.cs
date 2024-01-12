using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskViewModelBaseManager : TaskViewModelBaseStartable
    {
        #region SimpleProperties

        /// <summary>
        /// Id группы
        /// </summary>
        public Guid? ParentPriceEngineeringTasksId
        {
            get => Model.ParentPriceEngineeringTasksId;
            set => SetValue(value);
        }
        public Guid ParentPriceEngineeringTasksIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParentPriceEngineeringTasksId));
        public bool ParentPriceEngineeringTasksIdIsChanged => GetIsChanged(nameof(ParentPriceEngineeringTasksId));


        #endregion

        #region ComplexProperties

        /// <summary>
        /// Бюро конструкторов
        /// </summary>
        public new DesignDepartmentEmptyWrapper DesignDepartment
        {
            get => GetWrapper<DesignDepartmentEmptyWrapper>();
            set => SetComplexValue<DesignDepartment, DesignDepartmentEmptyWrapper>(DesignDepartment, value);
        }

        #endregion

        #region ctors

        protected TaskViewModelBaseManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        protected TaskViewModelBaseManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }

        #endregion

        public override void InitializeComplexProperties()
        {
            base.InitializeComplexProperties();
            InitializeComplexProperty(nameof(DesignDepartment), Model.DesignDepartment == null ? null : new DesignDepartmentEmptyWrapper(Model.DesignDepartment));
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.DesignDepartment == null)
                //для задач запущенных только на открытие производства из ТСЕ бюро назначать не обязательно
                if (this is TaskViewModelManager vm && vm.IsJustForProduction)
                {
                }
                else
                    yield return new ValidationResult($"{nameof(DesignDepartment)} is required", new []{nameof(DesignDepartment)});
        }
    }
}