using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    //[RibbonTab(typeof(TabSalesChart))]
    public partial class DirectumTaskRouteView : ViewBaseConfirmNavigationRequest
    {
        private readonly DirectumTaskRouteViewModel _viewModel;

        public DirectumTaskRouteView(DirectumTaskRouteViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
        }


        //public override bool IsNavigationTarget(NavigationContext navigationContext)
        //{
        //    return false;
        //}

        //public override void OnNavigatedTo(NavigationContext navigationContext)
        //{
        //    if (navigationContext.Parameters.Any())
        //    {
        //        var taskRoute = (DirectumTaskRoute)navigationContext.Parameters.First().Value;
        //        _viewModel.Load(taskRoute);
        //    }
        //    else
        //    {
        //        _viewModel.Load();
        //    }


        //    base.OnNavigatedTo(navigationContext);
        //}


        protected override bool IsSomethingChanged()
        {
            return _viewModel.DirectumTaskRoute.IsChanged;
        }
    }
}
