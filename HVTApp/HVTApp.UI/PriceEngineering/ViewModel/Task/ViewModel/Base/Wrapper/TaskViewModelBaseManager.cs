using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    public abstract class TaskViewModelBaseManager : TaskViewModelBaseWithStartCommand
    {
        #region SimpleProperties

        /// <summary>
        /// Id ������
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
        /// ���� �������������
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
    }
}