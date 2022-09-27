using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering
{
    public class WorkloadOnEmployeesViewModel : ViewModelBase
    {
        public IEnumerable<WorkloadItem> Items { get; }
        public WorkloadOnEmployeesViewModel(IUnityContainer container) : base(container)
        {
            //КБ, которыми руководит пользователь
            var departments = UnitOfWork.Repository<DesignDepartment>().Find(department => department.Head.Id == GlobalAppProperties.User.Id);
            
            //сотрудники из этих КБ
            var employees = departments.SelectMany(department => department.Staff).Distinct();

            Items = new List<WorkloadItem>(employees.OrderBy(x => x.ToString()).Select(x => new WorkloadItem(x, container)));
        }
    }

    public class WorkloadItem : BindableBase
    {
        private IUnitOfWork _unitOfWork;
        private readonly Guid _userId;

        public string Name { get; }

        public int Workload
        {
            get
            {
                return _unitOfWork.Repository<PriceEngineeringTask>().Find(priceEngineeringTask =>
                    priceEngineeringTask.UserConstructor != null &&
                    priceEngineeringTask.UserConstructor.Id == _userId &&
                    priceEngineeringTask.InProcessByConstructor)
                    .Count;
            }
        }

        public WorkloadItem(User user, IUnityContainer container)
        {
            _userId = user.Id;
            Name = user.Employee.Person.ToString();

            _unitOfWork = container.Resolve<IUnitOfWork>();
            container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Subscribe(OnSavePriceEngineeringTaskEvent);
        }

        private void OnSavePriceEngineeringTaskEvent(PriceEngineeringTask priceEngineeringTask)
        {
            if (priceEngineeringTask.UserConstructor?.Id == _userId)
            {
                RaisePropertyChanged(nameof(Workload));
            }
        }
    }
}