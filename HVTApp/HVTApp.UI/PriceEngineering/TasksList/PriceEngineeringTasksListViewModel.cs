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

        /// <summary>
        /// КБ, где текущий пользователь является руководителем
        /// </summary>
        public List<DesignDepartment> Departments { get; }

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

            Departments = UnitOfWork.Repository<DesignDepartment>().Find(x => x.Head.Id == GlobalAppProperties.User.Id);

            Load();
        }

        public override void Load()
        {
            var priceEngineeringTasks = new List<PriceEngineeringTasks>();

            //для менеджера
            if (UserIsManager)
            {
                priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(engineeringTasks => engineeringTasks.UserManager.Id == GlobalAppProperties.User.Id);
            }

            //для руководителя бюро ОГК
            if (UserIsDesignDepartmentHead)
            {
                if (Departments.Any())
                {
                    priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(engineeringTasks => engineeringTasks.GetDepartments().Intersect(Departments).Any());
                }
            }

            //для конструктора
            if (UserIsConstructor)
            {
                priceEngineeringTasks = UnitOfWork.Repository<PriceEngineeringTasks>().Find(engineeringTasks => engineeringTasks.GetSuitableTasksForWork(GlobalAppProperties.User).Any());
            }

            this.Load(priceEngineeringTasks);
        }
    }
}