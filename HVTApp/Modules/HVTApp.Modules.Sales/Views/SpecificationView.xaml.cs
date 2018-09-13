using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabSave)), RibbonTab(typeof(TabCrudUnits))]
    public partial class SpecificationView
    {
        private readonly SpecificationViewModel _viewModel;
        public SpecificationView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<SpecificationViewModel>();
            this.DataContext = _viewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var specification = (Specification)navigationContext.Parameters.First().Value;
            if (navigationContext.Parameters.Count() == 2)
            {
                var units = (IEnumerable<SalesUnit>)navigationContext.Parameters.Last().Value;
                await _viewModel.LoadAsync(specification, units);
                return;
            }
            await _viewModel.LoadAsync(specification);
        }
    }
}

