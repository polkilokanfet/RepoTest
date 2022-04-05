using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelDesignDepartmentHead : PriceEngineeringTaskViewModel
    {
        public DesignDepartment Department { get; private set; }

        /// <summary>
        /// Поручить проработку задачи
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; private set; }

        #region ctors

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
        }
        
        #endregion

        public override bool IsTarget => Department != null && Department.ProductBlockIsSuitable(Model.ProductBlockEngineer);

        public override bool IsEditMode => IsTarget;

        protected override void InCtor()
        {
            base.InCtor();

            //устанавливаем бюро ОГК
            Department = UnitOfWork.Repository<DesignDepartment>().Find(department => department.Head.Id == GlobalAppProperties.User.Id).Single();

            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    if (Department == null) return;

                    var user = Container.Resolve<ISelectService>().SelectItem(Department.Staff);

                    if (user == null) return;
                        
                    this.UserConstructor = new UserEmptyWrapper(user);
                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id), 
                        Message = $"Назначен исполнитель: {user}"
                    }));

                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                }, 
                () => IsTarget);
        }
    }
}