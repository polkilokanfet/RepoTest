using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.ViewModel;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManagerNew : PriceEngineeringTaskViewModelManager
    {
        /// <summary>
        /// ��������� �����, � ������� ������� ��� ������
        /// </summary>
        private readonly PriceEngineeringTasksViewModelManager _priceEngineeringTasksViewModelManager;

        public DelegateLogConfirmationCommand RemoveTaskCommand { get; }

        public DelegateLogCommand SelectDesignDepartmentCommand { get; }


        /// <summary>
        /// ��� �������� ����� �������-����������� ���������� �� �������� ������
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="salesUnits"></param>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTasksViewModelManager">��������� �����, � ������� ������� ��� ������</param>
        public PriceEngineeringTaskViewModelManagerNew(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits, PriceEngineeringTasksViewModelManager priceEngineeringTasksViewModelManager) 
            : this(container, unitOfWork, salesUnits.First().Product)
        {
            _priceEngineeringTasksViewModelManager = priceEngineeringTasksViewModelManager;
            this.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit)));
        }

        /// <summary>
        /// ��� �������� ����� ������ �� ��������
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="product"></param>
        public PriceEngineeringTaskViewModelManagerNew(IUnityContainer container, IUnitOfWork unitOfWork, Product product) 
            : base(container, unitOfWork)
        {
            ProductBlockEngineer = new ProductBlockStructureCostWrapper(product.ProductBlock);
            ProductBlockManager = new ProductBlockEmptyWrapper(product.ProductBlock);

            //����
            var department = UnitOfWork.Repository<DesignDepartment>().Find(designDepartment => designDepartment.ProductBlockIsSuitable(this.ProductBlockEngineer.Model)).FirstOrDefault();
            if (department != null)
            {
                this.DesignDepartment = new DesignDepartmentEmptyWrapper(department);
            }

            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(new List<PriceEngineeringTaskViewModel>());
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
            
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    var vm = new PriceEngineeringTaskViewModelManagerNew(container, unitOfWork, dependentProduct.Product);
                    this.ChildPriceEngineeringTasks.Add(vm);
                    vm.Parent = this;
                }
            }

            RemoveTaskCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "�� �������, ��� ������ ������� ��� ������ �� ������?",
                () =>
                {
                    _priceEngineeringTasksViewModelManager?.RemoveChildTask(this);
                },
                () =>
                    _priceEngineeringTasksViewModelManager != null &&
                    _priceEngineeringTasksViewModelManager.AllowEditProps &&
                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Id) == null);

            SelectDesignDepartmentCommand = new DelegateLogCommand(
                () =>
                {
                    var departments = UnitOfWork.Repository<DesignDepartment>().Find(department1 => department1.ProductBlockIsSuitable(this.Model.ProductBlockEngineer));
                    var designDepartment = Container.Resolve<ISelectService>().SelectItem(departments);
                    if (designDepartment != null)
                    {
                        this.DesignDepartment = new DesignDepartmentEmptyWrapper(UnitOfWork.Repository<DesignDepartment>().GetById(designDepartment.Id));
                    }
                },
                () => IsEditMode);

            //������ � �������� ��������, ����� �������� ��������������� ������
            this.Statuses.Add(new PriceEngineeringTaskStatusEmptyWrapper(new PriceEngineeringTaskStatus { StatusEnum = PriceEngineeringTaskStatusEnum.Created }));
        }
    }
}