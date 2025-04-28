using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class TaskViewModelObserver : TaskViewModel
    {
        public override bool IsTarget =>
            Model.DesignDepartment != null &&
            Model.DesignDepartment.Observers.ContainsById(GlobalAppProperties.User);

        public TaskViewModelObserver(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId)
        {
        }

        public TaskViewModelObserver(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
        }
    }
}