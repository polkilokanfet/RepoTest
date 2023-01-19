using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelBase : WrapperBase<PriceEngineeringTask>
    {
        protected readonly IUnitOfWork UnitOfWork;

        #region ctors

        private TaskViewModelBase(IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask)
            : base(priceEngineeringTask)
        {
            UnitOfWork = unitOfWork;
        }

        protected TaskViewModelBase(IUnitOfWork unitOfWork, Guid priceEngineeringTaskId)
            : this(unitOfWork, unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId))
        {
        }

        protected TaskViewModelBase(IUnitOfWork unitOfWork)
            : this(unitOfWork, new PriceEngineeringTask())
        {
        }

        #endregion
    }
}