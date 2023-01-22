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
    public class TaskViewModelManagerNew : TaskViewModelManager
    {
        /// <summary>
        ///  онтейнер задач, в которую вложена эта задача
        /// </summary>
        private readonly TasksViewModelManager _tasksViewModelManager;

        public DelegateLogConfirmationCommand RemoveTaskCommand { get; }

        public DelegateLogCommand SelectDesignDepartmentCommand { get; }


        /// <summary>
        /// ƒл€ создани€ новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="salesUnits"></param>
        /// <param name="container"></param>
        /// <param name="tasksViewModelManager"> онтейнер задач, в которую вложена эта задача</param>
        public TaskViewModelManagerNew(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits, TasksViewModelManager tasksViewModelManager) 
            : this(container, unitOfWork, salesUnits.First().Product)
        {
            _tasksViewModelManager = tasksViewModelManager;
            this.SalesUnits.AddRange(salesUnits.Select(salesUnit => new SalesUnitEmptyWrapper(salesUnit)));
        }

        /// <summary>
        /// ƒл€ создани€ новой задачи по продукту
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="product"></param>
        public TaskViewModelManagerNew(IUnityContainer container, IUnitOfWork unitOfWork, Product product) 
            : base(container, unitOfWork)
        {
            this.Model.ProductBlockEngineer = product.ProductBlock;
            this.Model.ProductBlockManager = product.ProductBlock;

            //бюро
            var department = UnitOfWork.Repository<DesignDepartment>().Find(designDepartment => designDepartment.ProductBlockIsSuitable(this.Model.ProductBlockEngineer)).FirstOrDefault();
            if (department != null)
            {
                this.DesignDepartment = new DesignDepartmentEmptyWrapper(department);
            }

            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<TaskViewModel>(new List<TaskViewModel>());
            RegisterCollection(ChildPriceEngineeringTasks, Model.ChildPriceEngineeringTasks);
            
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    var vm = new TaskViewModelManagerNew(container, unitOfWork, dependentProduct.Product);
                    this.ChildPriceEngineeringTasks.Add(vm);
                    vm.Parent = this;
                }
            }

            #region Commands

            RemoveTaskCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "¬ы уверены, что хотите удалить эту задачу из списка?",
                () =>
                {
                    _tasksViewModelManager?.RemoveChildTask(this);
                },
                () =>
                    _tasksViewModelManager != null &&
                    _tasksViewModelManager.AllowEditProps &&
                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(this.Model.Id) == null);

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

            #endregion

            //задача в процессе создани€, нужно добавить соответствующий статус
            this.Statuses.Add(new PriceEngineeringTaskStatusEmptyWrapper(new PriceEngineeringTaskStatus { StatusEnum = PriceEngineeringTaskStatusEnum.Created }));
        }
    }
}