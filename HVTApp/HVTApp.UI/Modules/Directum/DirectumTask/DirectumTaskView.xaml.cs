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
    [RibbonTab(typeof(TabDirectumTask))]
    public partial class DirectumTaskView : ViewBaseConfirmNavigationRequest, IDisposable
    {
        private DirectumTaskViewModel _viewModel;

        public DirectumTaskView(DirectumTaskViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
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
            if (navigationContext.Parameters.Any())
            {
                if (navigationContext.Parameters.Count() == 1)
                {
                    if (navigationContext.Parameters.First().Value is Model.POCOs.DirectumTask directumTask)
                        _viewModel.Load(directumTask);

                    if (navigationContext.Parameters.First().Value is DirectumTaskGroup directumTaskGroup)
                        _viewModel.Load(directumTaskGroup);
                }

                if (navigationContext.Parameters.Count() == 2)
                {
                    var parentTask = (Model.POCOs.DirectumTask)navigationContext.Parameters[nameof(Model.POCOs.DirectumTask)];
                    var isSubTask = (bool) navigationContext.Parameters[nameof(bool.GetType)];
                    _viewModel.Load(parentTask, isSubTask);
                }
            }
            else
            {
                _viewModel.Load();
            }

            base.OnNavigatedTo(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            this.Dispose();

            //if (RegionManager.Regions[RegionNames.ContentRegion].Views.Contains(this))
            //{
            //    RegionManager.Regions[RegionNames.ContentRegion].Remove(this);
            //}
        }

        protected override bool IsSomethingChanged()
        {
            return _viewModel.DirectumTask?.IsChanged ?? false;
        }

        public void Dispose()
        {
            _viewModel.Dispose();
            _viewModel = null;
            this.DataContext = null;
        }
    }
}
