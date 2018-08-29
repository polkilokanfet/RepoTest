using System;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabCRUD))]
    [RibbonTab(typeof(TabOffer))]
    public partial class OffersView
    {
        private readonly OffersViewModel _offersViewModel;

        public OffersView(OffersViewModel offersViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _offersViewModel = offersViewModel;
            this.DataContext = _offersViewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _offersViewModel.LoadAsync();
            Loaded -= OnLoaded;
        }
    }
}
