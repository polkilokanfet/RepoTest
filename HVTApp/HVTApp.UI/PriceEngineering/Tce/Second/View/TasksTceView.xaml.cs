using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View
{
    [RibbonTab(typeof(TabTaskTceView))]
    public partial class TasksTceView : IDisposable
    {
        private readonly TasksTceViewModel _viewModel;

        public TasksTceView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            InitializeComponent();

            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    _viewModel = container.Resolve<TasksTceViewModelFrontManager>();
                    break;
                }
                case Role.BackManager:
                {
                    _viewModel = container.Resolve<TasksTceViewModelBackManager>();
                    break;
                }
                case Role.BackManagerBoss:
                {
                    _viewModel = container.Resolve<TasksTceViewModelBackManagerBoss>();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
                var parameter = navigationContext.Parameters.First().Value;
                if (parameter is PriceEngineeringTasks priceEngineeringTasks)
                    _viewModel.Load(priceEngineeringTasks);
                if (parameter is PriceEngineeringTask priceEngineeringTask)
                    _viewModel.Load(priceEngineeringTask);
            }

            base.OnNavigatedTo(navigationContext);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
            this.Dispose();
        }

        protected override bool IsSomethingChanged()
        {
            return _viewModel?.Item.IsChanged ?? false;
        }

        public void Dispose()
        {
            _viewModel.Dispose();
            //_viewModel = null;
            this.DataContext = null;

            //GC.SuppressFinalize(this);
        }
    }
}
