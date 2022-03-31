using System.Collections.Generic;
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

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask)
            : base(container, unitOfWork, priceEngineeringTask)
        {
            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var department = UnitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var user = container.Resolve<ISelectService>().SelectItem(department.Staff);

                    if (user != null)
                    {
                        this.UserConstructor = new UserEmptyWrapper(user);
                    }
                });
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits)
            : base(container, unitOfWork, salesUnits)
        {
            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var department = UnitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var user = container.Resolve<ISelectService>().SelectItem(department.Staff);

                    if (user != null)
                    {
                        this.UserConstructor = new UserEmptyWrapper(user);
                    }
                });
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, Product product)
            : base(container, unitOfWork, product)
        {
            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var department = UnitOfWork.Repository<DesignDepartment>()
                        .Find(designDepartment => designDepartment.ProductBlockIsSuitable(ProductBlockManager.Model))
                        .FirstOrDefault();

                    if (department == null)
                        return;

                    var user = container.Resolve<ISelectService>().SelectItem(department.Staff);

                    if (user != null)
                    {
                        this.UserConstructor = new UserEmptyWrapper(user);
                    }
                });
        }
    }
}