using System;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Tabs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    [RibbonTab(typeof(TabOffer))]
    public partial class OfferRedactorView
    {
        private readonly IUnityContainer _container;
        private readonly OfferDetailsViewModel _viewModel;

        public OfferRedactorView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _container = container;
            InitializeComponent();
            _viewModel = container.Resolve<OfferDetailsViewModel>();
            ContentControl.Content = container.Resolve<OfferView>();
            //ContentControl.DataContext = _viewModel;
            this.DataContext = _viewModel;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var offer = (Offer) navigationContext.Parameters.First().Value;
            await _viewModel.LoadAsync(offer);
        }
    }
}
