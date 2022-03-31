using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.View
{
    //[RibbonTab(typeof(TabDirectumTask))]
    public partial class PriceEngineeringTasksView : ViewBaseConfirmNavigationRequest, IDisposable
    {
        private PriceEngineeringTasksViewModel _viewModel;

        public PriceEngineeringTasksView(PriceEngineeringTasksViewModel viewModel, IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(container, regionManager, eventAggregator)
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
                    if (navigationContext.Parameters.First().Value is PriceEngineeringTask priceEngineeringTask)
                        _viewModel.Load(priceEngineeringTask);

                    if (navigationContext.Parameters.First().Value is IEnumerable<SalesUnit> salesUnits)
                        _viewModel.Load(salesUnits);
                }
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
            return _viewModel.PriceEngineeringTaskViewModels.Any() && _viewModel.PriceEngineeringTaskViewModels.Any(x => x.IsChanged);
        }

        public void Dispose()
        {
            _viewModel.Dispose();
            _viewModel = null;
            this.DataContext = null;

            GC.SuppressFinalize(this);
        }
    }
}
