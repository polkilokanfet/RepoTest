using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Reports.Tabs;
using HVTApp.UI.Modules.Reports.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.Views
{
    [RibbonTab(typeof(TabFakeData))]
    public partial class FakeDataView
    {
        private readonly FakeDataViewModel _viewModel;
        public FakeDataView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<FakeDataViewModel>();
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var salesUnits = navigationContext.Parameters.First().Value as IEnumerable<SalesUnit>;
            await _viewModel.Load(salesUnits);

            base.OnNavigatedTo(navigationContext);
        }
    }
}
