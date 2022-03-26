using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Directum;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    //[RibbonTab(typeof(TabDirectumTask))]
    public partial class PriceEngineeringTaskView : ViewBaseConfirmNavigationRequest, IDisposable
    {
        private PriceEngineeringTaskViewModel _viewModel;

        public PriceEngineeringTaskView(PriceEngineeringTaskViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            _viewModel = viewModel;
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //if (navigationContext.Parameters.Any())
            //{
            //    if (navigationContext.Parameters.Count() == 1)
            //    {
            //        if (navigationContext.Parameters.First().Value is Model.POCOs.DirectumTask directumTask)
            //            _viewModel.Load(directumTask);

            //        if (navigationContext.Parameters.First().Value is DirectumTaskGroup directumTaskGroup)
            //            _viewModel.Load(directumTaskGroup);
            //    }

            //    if (navigationContext.Parameters.Count() == 2)
            //    {
            //        var parentTask = (Model.POCOs.DirectumTask)navigationContext.Parameters[nameof(Model.POCOs.DirectumTask)];
            //        var isSubTask = (bool) navigationContext.Parameters[nameof(bool.GetType)];
            //        _viewModel.Load(parentTask, isSubTask);
            //    }
            //}
            //else
            //{
            //    _viewModel.Load();
            //}

            //base.OnNavigatedTo(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //base.OnNavigatedFrom(navigationContext);
            //this.Dispose();
        }

        protected override bool IsSomethingChanged()
        {
            return _viewModel?.PriceEngineeringTaskWrapper?.IsChanged ?? false;
        }

        public void Dispose()
        {
            //_viewModel.Dispose();
            //_viewModel = null;
            //this.DataContext = null;

            //GC.SuppressFinalize(this);
        }
    }
}
