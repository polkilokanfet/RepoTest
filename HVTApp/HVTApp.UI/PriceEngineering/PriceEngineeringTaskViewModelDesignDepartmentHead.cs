using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelDesignDepartmentHead : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// Поручить проработку задачи
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork)
        {
            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var department = unitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(PriceEngineeringTaskWrapper.ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var user = container.Resolve<ISelectService>().SelectItem(department.Staff);

                    if (user != null)
                    {
                        this.PriceEngineeringTaskWrapper.UserConstructor = new UserWrapper(user);
                    }
                });
        }
    }
}