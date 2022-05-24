using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Tce.Tabs;
using HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    [RibbonTab(typeof(TabPriceEngineeringTaskTce))]
    public partial class PriceEngineeringTaskTceView : ViewBaseConfirmNavigationRequest, IDisposable
    {
        private readonly PriceEngineeringTaskTceViewModel _viewModel;

        public PriceEngineeringTaskTceView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
        {
            InitializeComponent();

            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    _viewModel = container.Resolve<PriceEngineeringTaskTceViewModelFrontManager>();
                    break;
                }
                case Role.BackManager:
                {
                    _viewModel = container.Resolve<PriceEngineeringTaskTceViewModelBackManager>();
                    break;
                }
                case Role.BackManagerBoss:
                {
                    _viewModel = container.Resolve<PriceEngineeringTaskTceViewModelBackManagerBoss>();
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
                if (parameter is IEnumerable<PriceEngineeringTask> priceEngineeringTasks)
                    _viewModel.Create(priceEngineeringTasks);
                else if (parameter is PriceEngineeringTaskTce priceEngineeringTaskTce)
                    _viewModel.Load(priceEngineeringTaskTce);
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
