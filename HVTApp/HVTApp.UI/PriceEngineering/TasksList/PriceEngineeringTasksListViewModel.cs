using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksListViewModel : PriceEngineeringTasksLookupListViewModel
    {
        public bool UserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool UserIsDesignDepartmentHead => GlobalAppProperties.User.RoleCurrent == Role.DesignDepartmentHead;
        public bool UserIsConstructor => GlobalAppProperties.User.RoleCurrent == Role.Constructor;

        public DesignDepartment Department { get; private set; }

        public DelegateLogCommand OpenTaskCommand { get; }

        public PriceEngineeringTasksListViewModel(IUnityContainer container) : base(container)
        {
            OpenTaskCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksView>(new NavigationParameters
                    {
                        { nameof(PriceEngineeringTasks), this.SelectedItem }
                    });
                }, 
                () => SelectedLookup != null);
            this.SelectedLookupChanged += lookup => OpenTaskCommand.RaiseCanExecuteChanged();

            Load();
        }

        public override void Load()
        {
            var priceEngineeringTasks = new List<PriceEngineeringTasks>();

            //для менеджера
            if (UserIsManager)
            {
                priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(x => x.UserManager.Id == GlobalAppProperties.User.Id);
            }

            //для руководителя бюро ОГК
            if (UserIsDesignDepartmentHead)
            {
                Department = UnitOfWork.Repository<DesignDepartment>().Find(department => Equals(department.Head.Id, GlobalAppProperties.User.Id)).FirstOrDefault();
                if (Department != null)
                {
                    priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(x => x.GetSuitableTasksForInstruct(Department).Any());
                }
            }

            //для конструктора
            if (UserIsConstructor)
            {
                priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(x => x.GetSuitableTasksForWork(GlobalAppProperties.User).Any());
            }


            this.Load(priceEngineeringTasks);
        }
    }
}