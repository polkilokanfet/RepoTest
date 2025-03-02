using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Project1.Views
{
    [RibbonTab(typeof(TabProject))]
    public partial class ProjectView1 : ViewBaseConfirmNavigationRequest
    {
        public ProjectView1(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            InitializeComponent();
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //если грузится существующий проект
            if (navigationContext.Parameters != null && 
                navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.Count() == 1)
                {
                    //редактирование существующего проекта
                    if (navigationContext.Parameters.First().Value is Project project)
                    {
                        this.DataContext = new ProjectViewModel1(project, Container);
                    }

                    ////перенос оборудования в новый проект
                    //if (navigationContext.Parameters.First().Value is List<SalesUnit>)
                    //{
                    //    var units = (List<SalesUnit>)navigationContext.Parameters.First().Value;
                    //    _viewModel.LoadForMove(units);
                    //}
                }
                else if (navigationContext.Parameters.Count() == 2)
                {
                    ////перенос оборудования в существующий проект
                    //var project = (Project)navigationContext.Parameters.First().Value;
                    //var units = (List<SalesUnit>)navigationContext.Parameters.Last().Value;
                    //_viewModel.LoadForMove(units, project);
                }
            }
            //если грузится новый проект
            else
            {
                this.DataContext = new ProjectViewModel1(Container);
            }
        }

        protected override bool IsSomethingChanged()
        {
            if (this.DataContext is ProjectViewModel1 vm)
            {
                return vm.ProjectWrapper.IsChanged &&
                       vm.ProjectWrapper.IsValid;
            }

            throw new NotImplementedException();
        }
    }
}

