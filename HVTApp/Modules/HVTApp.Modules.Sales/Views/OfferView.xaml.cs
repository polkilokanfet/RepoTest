using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using TabSave = HVTApp.Modules.Sales.Tabs.TabSave;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabSave)), RibbonTab(typeof(TabCrudUnits))]
    public partial class OfferView
    {
        private readonly OfferDetailsViewModel _viewModel;

        public OfferView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = container.Resolve<OfferDetailsViewModel>();
            this.DataContext = _viewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var offer = (Offer)navigationContext.Parameters.First().Value;
            if (navigationContext.Parameters.Count() == 2)
            {
                var units = (IEnumerable<OfferUnit>)navigationContext.Parameters.Last().Value;
                await _viewModel.LoadAsync(offer, units);
                return;
            }
            await _viewModel.LoadAsync(offer);
        }

    }
}
